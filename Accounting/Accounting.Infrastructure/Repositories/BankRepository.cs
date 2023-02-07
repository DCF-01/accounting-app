using Accounting.Common;
using Accounting.Infrastructure.Data;
using Accounting.Infrastructure.Models;
using Accounting.Infrastructure.Extensions;
using Accounting.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Accounting.Infrastructure.Repositories;

public class BankRepository : IBankRepository
{
    private readonly ApplicationDbContext _ctx;

    public BankRepository(ApplicationDbContext ctx)
    {
        _ctx = ctx;
    }

    public async Task CreateAsync(Guid masterCompanyId, Bank bank)
    {
        bank.MasterCompanyId = masterCompanyId;
        foreach (var acc in bank.BankAccounts)
        {
            acc.MasterCompanyId = masterCompanyId;
            acc.BankId = bank.BankId;
        }

        _ctx.BankAccounts.AttachRange(bank.BankAccounts);
        _ctx.Banks.Add(bank);

        await _ctx.SaveChangesAsync();
    }

    public async Task UpdateAsync(Guid masterCompanyId, Bank bank)
    {
        var newAccountIds = bank.BankAccounts.Select(acc => acc.BankAccountId).ToList();

        var existingBank =
            await _ctx.Banks
                .Include(b => b.BankAccounts)
                .Where(b =>
                    b.BankId == bank.BankId &&
                    b.MasterCompanyId == masterCompanyId
                )
                .SingleOrDefaultAsync();

        existingBank.Name = bank.Name;
        existingBank.SWIFT = bank.SWIFT;
        existingBank.BankAccounts = await _ctx.BankAccounts
            .Where(ba => ba.MasterCompanyId == masterCompanyId)
            .Where(ba => newAccountIds.Contains(ba.BankAccountId))
            .ToListAsync();

        await _ctx.SaveChangesAsync();
    }


    public async Task<Bank> GetAsync(Guid masterCompanyId, int id)
    {
        Bank bank;

        if (id >= 0)
        {
            bank =
                await _ctx.Banks
                    .Where(b => b.BankId == id)
                    .Where(b => b.MasterCompanyId == masterCompanyId)
                    .SingleOrDefaultAsync();

            bank.BankAccounts =
                await _ctx.BankAccounts
                    .Where(ba => ba.MasterCompanyId == masterCompanyId)
                    .ToListAsync();
        }
        else
        {
            bank = new Bank
            {
                BankAccounts = await _ctx.BankAccounts
                    .Where(ba => ba.MasterCompanyId == masterCompanyId)
                    .ToListAsync()
            };
        }

        return bank;
    }

    public async Task<IEnumerable<Bank>> GetAllAsync(Guid masterCompanyId)
    {
        return await _ctx.Banks.Where(b => b.MasterCompanyId == masterCompanyId).ToListAsync();
    }

    public async Task DeleteAsync(Guid masterCompanyId, int id)
    {
        _ctx.Banks.Remove(new Bank
        {
            BankId = id,
            MasterCompanyId = masterCompanyId
        });
        await _ctx.SaveChangesAsync();
    }

    public async Task<PagedResult<Bank>> GetPagedAsync(Guid MasterCompanyId, PagingModel pagingModel)
    {
        var query =
            _ctx.Banks
                .Include(bank => bank.BankAccounts)
                .Where(bank =>
                    bank.Name.Contains(pagingModel.SearchTerm) &&
                    bank.MasterCompanyId == MasterCompanyId
                );

        var result =
            await query
                .Select(bank => new Bank
                {
                    BankId = bank.BankId,
                    Name = bank.Name,
                    SWIFT = bank.SWIFT,
                })
                .Order(pagingModel.SortOrder, pagingModel.SortColumn)
                .Skip(pagingModel.PageSize * pagingModel.Page)
                .Take(pagingModel.PageSize)
                .ToListAsync();

        return new PagedResult<Bank>(result, await query.CountAsync());
    }
}
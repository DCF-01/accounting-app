using Accounting.Common;
using Accounting.Infrastructure.Data;
using Accounting.Infrastructure.Models;
using Accounting.Infrastructure.Repositories.Interfaces;
using Accounting.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Accounting.Infrastructure.Repositories;

public class BankAccountRepository : IBankAccountRepository
{
    private readonly ApplicationDbContext _ctx;

    public BankAccountRepository(ApplicationDbContext ctx)
    {
        _ctx = ctx;
    }

    public async Task CreateAsync(Guid MasterCompanyId, BankAccount bankAccount)
    {
        bankAccount.MasterCompanyId = MasterCompanyId;
        _ctx.BankAccounts.Add(bankAccount);
        await _ctx.SaveChangesAsync();
    }

    public async Task UpdateAsync(Guid MasterCompanyId, BankAccount bankAccount)
    {
        bankAccount.MasterCompanyId = MasterCompanyId;
        _ctx.BankAccounts.Update(bankAccount);
        await _ctx.SaveChangesAsync();
    }

    public async Task<BankAccount> GetAsync(Guid MasterCompanyId, int id)
    {
        return await
            _ctx.BankAccounts
                .Where(bankAccount => bankAccount.BankAccountId == id)
                .Where(bankAccount => bankAccount.MasterCompanyId == MasterCompanyId)
                .SingleOrDefaultAsync();
    }

    public async Task DeleteAsync(Guid MasterCompanyId, int id)
    {
        _ctx.BankAccounts.Remove(new()
            {
                BankAccountId = id,
                MasterCompanyId = MasterCompanyId
            }
        );
        await _ctx.SaveChangesAsync();
    }

    public async Task<PagedResult<BankAccount>> GetPagedAsync(Guid MasterCompanyId, PagingModel pagingModel)
    {
        var query =
            _ctx.BankAccounts
                .Include(bankAccount => bankAccount.Bank)
                .Where(bankAccount =>
                    bankAccount.Name.Contains(pagingModel.SearchTerm) &&
                    bankAccount.MasterCompanyId == MasterCompanyId
                );

        var result =
            await query
                .Select(bankAccount => new BankAccount
                {
                    BankId = bankAccount.BankId,
                    Name = bankAccount.Name,
                    Number = bankAccount.Number,
                    Bank = bankAccount.Bank,
                    BankAccountId = bankAccount.BankAccountId,
                    IBAN = bankAccount.IBAN
                })
                .Order(pagingModel.SortOrder, pagingModel.SortColumn)
                .Skip(pagingModel.PageSize * pagingModel.Page)
                .Take(pagingModel.PageSize)
                .ToListAsync();

        return new PagedResult<BankAccount>(result, await query.CountAsync());
    }
}
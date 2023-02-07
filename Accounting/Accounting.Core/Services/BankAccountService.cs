using Accounting.Common;
using Accounting.Core.Interfaces;
using Accounting.Core.Requests;
using Accounting.Infrastructure.Models;
using Accounting.Infrastructure.Repositories.Interfaces;
using AutoMapper;

namespace Accounting.Core.Services;

public class BankAccountService : IBankAccountService
{
    private readonly IMapper _mapper;
    private readonly IUserInformation _userInformation;
    private readonly IBankAccountRepository _bankAccountRepository;

    public BankAccountService(IMapper mapper, IUserInformation userInformation,
        IBankAccountRepository bankAccountRepository)
    {
        _mapper = mapper;
        _userInformation = userInformation;
        _bankAccountRepository = bankAccountRepository;
    }

    public async Task CreateAsync(BankAccountPost bankAccountPost)
    {
        await _bankAccountRepository
            .CreateAsync(_userInformation.MasterCompanyId, _mapper.Map<BankAccount>(bankAccountPost));
    }

    public async Task UpdateAsync(BankAccountPut bankAccountPut)
    {
        await _bankAccountRepository
            .UpdateAsync(_userInformation.MasterCompanyId, _mapper.Map<BankAccount>(bankAccountPut));
    }

    public async Task<BankAccountGet> GetAsync(int id)
    {
        var result = await _bankAccountRepository.GetAsync(_userInformation.MasterCompanyId, id);
        return _mapper.Map<BankAccountGet>(result);
    }

    public async Task DeleteAsync(int id)
    {
        await _bankAccountRepository.DeleteAsync(_userInformation.MasterCompanyId, id);
    }

    public async Task<PagedResult<BankAccountGet>> GetPagedAsync(PagingModel pagingModel)
    {
        var result = await _bankAccountRepository.GetPagedAsync(_userInformation.MasterCompanyId, pagingModel);
        return
            new PagedResult<BankAccountGet>(
                _mapper.Map<IEnumerable<BankAccountGet>>(result.Items),
                result.FilteredCount
            );
    }
}
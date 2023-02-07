using Accounting.Common;
using Accounting.Core.Interfaces;
using Accounting.Core.Requests;
using Accounting.Infrastructure.Models;
using Accounting.Infrastructure.Repositories.Interfaces;
using AutoMapper;

namespace Accounting.Core.Services;

public class BankService : IBankService
{
    private readonly IUserInformation _userInformation;
    private readonly IMapper _mapper;
    private readonly IBankRepository _bankRepository;

    public BankService(IUserInformation userInformation, IMapper mapper, IBankRepository bankRepository)
    {
        _userInformation = userInformation;
        _mapper = mapper;
        _bankRepository = bankRepository;
    }


    public async Task CreateAsync(BankPost bank)
    {
        await _bankRepository.CreateAsync(_userInformation.MasterCompanyId, _mapper.Map<Bank>(bank));
    }

    public async Task UpdateAsync(BankPut bank)
    {
        await _bankRepository.UpdateAsync(_userInformation.MasterCompanyId, _mapper.Map<Bank>(bank));
    }

    public async Task<BankGet> GetAsync(int id = -1)
    {
        var result = await _bankRepository.GetAsync(_userInformation.MasterCompanyId, id);
        return _mapper.Map<BankGet>(result);
    }

    public async Task DeleteAsync(int id)
    {
        await _bankRepository.DeleteAsync(_userInformation.MasterCompanyId, id);
    }

    public async Task<PagedResult<BankGet>> GetPagedAsync(PagingModel pagingModel)
    {
        var result = await _bankRepository.GetPagedAsync(_userInformation.MasterCompanyId, pagingModel);
        return new PagedResult<BankGet>(_mapper.Map<IEnumerable<BankGet>>(result.Items), result.FilteredCount);
    }
}
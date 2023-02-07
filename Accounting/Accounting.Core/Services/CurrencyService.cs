using Accounting.Common;
using Accounting.Core.Interfaces;
using Accounting.Core.Requests;
using Accounting.Infrastructure.Models;
using Accounting.Infrastructure.Repositories.Interfaces;
using AutoMapper;

namespace Accounting.Core.Services;

public class CurrencyService : ICurrencyService
{
    private readonly IUserInformation _userInformation;
    private readonly IMapper _mapper;
    private readonly ICurrencyRepository _currencyRepository;

    public CurrencyService(IUserInformation userInformation, IMapper mapper, ICurrencyRepository currencyRepository)
    {
        _userInformation = userInformation;
        _mapper = mapper;
        _currencyRepository = currencyRepository;
    }

    public async Task CreateAsync(CurrencyPost currencyPost)
    {
        await _currencyRepository.CreateAsync(_userInformation.MasterCompanyId, _mapper.Map<Currency>(currencyPost));
    }

    public async Task UpdateAsync(CurrencyPut currencyPut)
    {
        await _currencyRepository.UpdateAsync(_userInformation.MasterCompanyId, _mapper.Map<Currency>(currencyPut));
    }

    public async Task<CurrencyGet> GetAsync(int id)
    {
        var result = await _currencyRepository.GetAsync(_userInformation.MasterCompanyId, id);
        return _mapper.Map<CurrencyGet>(result);
    }

    public async Task DeleteAsync(int id)
    {
        await _currencyRepository.DeleteAsync(_userInformation.MasterCompanyId, id);
    }

    public async Task<PagedResult<CurrencyGet>> GetPagedAsync(PagingModel pagingModel)
    {
        var result = await _currencyRepository.GetPagedAsync(_userInformation.MasterCompanyId, pagingModel);
        return
            new PagedResult<CurrencyGet>(
                _mapper.Map<IEnumerable<CurrencyGet>>(result.Items),
                result.FilteredCount
            );
    }
}
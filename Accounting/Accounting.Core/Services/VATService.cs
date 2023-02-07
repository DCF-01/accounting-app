using Accounting.Common;
using Accounting.Core.Interfaces;
using Accounting.Core.Requests;
using Accounting.Infrastructure.Models;
using Accounting.Infrastructure.Repositories.Interfaces;
using AutoMapper;

namespace Accounting.Core.Services;

public class VATService : IVATService
{
    private readonly IUserInformation _userInformation;
    private readonly IVATRepository _vatRepository;
    private readonly IMapper _mapper;

    public VATService(IUserInformation userInformation, IVATRepository vatRepository, IMapper mapper)
    {
        _userInformation = userInformation;
        _vatRepository = vatRepository;
        _mapper = mapper;
    }

    public async Task CreateAsync(VATPost vatPost)
    {
        await _vatRepository.CreateAsync(_userInformation.MasterCompanyId, _mapper.Map<VAT>(vatPost));
    }

    public async Task UpdateAsync(VATPut vatPut)
    {
        await _vatRepository.UpdateAsync(_userInformation.MasterCompanyId, _mapper.Map<VAT>(vatPut));
    }

    public async Task<VATGet> GetAsync(int id)
    {
        var result = await _vatRepository.GetAsync(_userInformation.MasterCompanyId, id);
        return _mapper.Map<VATGet>(result);
    }

    public async Task DeleteAsync(int id)
    {
        await _vatRepository.DeleteAsync(id);
    }

    public async Task<PagedResult<VATGet>> GetPagedAsync(PagingModel pagingModel)
    {
        var result = await _vatRepository.GetPagedAsync(_userInformation.MasterCompanyId, pagingModel);
        return new PagedResult<VATGet>(_mapper.Map<IEnumerable<VATGet>>(result.Items), result.FilteredCount);
    }
}
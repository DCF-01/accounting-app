using Accounting.Common;
using Accounting.Core.Enums;
using Accounting.Core.Interfaces;
using Accounting.Core.Requests;
using Accounting.Infrastructure.Models;
using Accounting.Infrastructure.Repositories.Interfaces;
using AutoMapper;

namespace Accounting.Core.Services;

public class CompanyService : ICompanyService
{
    private readonly IUserInformation _userInformation;
    private readonly IMapper _mapper;
    private readonly ICompanyRepository _companyRepository;
    private readonly IBankRepository _bankRepository;

    public CompanyService(IUserInformation userInformation, IMapper mapper, ICompanyRepository companyRepository, IBankRepository bankRepository)
    {
        _userInformation = userInformation;
        _mapper = mapper;
        _companyRepository = companyRepository;
        _bankRepository = bankRepository;
    }

    public async Task CreateAsync(CompanyPost companyPost)
    {
        await _companyRepository.CreateAsync(_userInformation.MasterCompanyId, _mapper.Map<Company>(companyPost));  
    }

    public async Task UpdateAsync(CompanyPut companyPut)
    {
        await _companyRepository.UpdateAsync(_userInformation.MasterCompanyId, _mapper.Map<Company>(companyPut));
    }

    public async Task<CompanyGet> GetAsync(int id, ViewType viewType = ViewType.Get)
    {
        CompanyGet company;

        switch (viewType)
        {
            case ViewType.Create:
                company = new CompanyGet();
                company.Banks = _mapper.Map<IEnumerable<BankGet>>(await _bankRepository.GetAllAsync(_userInformation.MasterCompanyId));
                break;
            case ViewType.Update:
                company = _mapper.Map<CompanyGet>(await _companyRepository.GetAsync(_userInformation.MasterCompanyId, id));
                company.Banks = _mapper.Map<IEnumerable<BankGet>>(await _bankRepository.GetAllAsync(_userInformation.MasterCompanyId));
                break;
            default:
                company = _mapper.Map<CompanyGet>(await _companyRepository.GetAsync(_userInformation.MasterCompanyId, id)); 
                break;
        }

        return company;
    }

    public async Task DeleteAsync(int id)
    {
        await _companyRepository.DeleteAsync(_userInformation.MasterCompanyId, id);
    }

    public async Task<PagedResult<CompanyGet>> GetPagedAsync(PagingModel pagingModel)
    {
        var result = await _companyRepository.GetPagedAsync(_userInformation.MasterCompanyId, pagingModel);

        return new PagedResult<CompanyGet>(_mapper.Map<IEnumerable<CompanyGet>>(result.Items), result.FilteredCount);
    }
}
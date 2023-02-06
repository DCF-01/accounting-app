using Accounting.Common;
using Accounting.Core.Interfaces;
using Accounting.Core.Requests;
using Accounting.Infrastructure.Repositories.Interfaces;
using AutoMapper;

namespace Accounting.Core.Services;

public class MasterCompanyService : IMasterCompanyService
{
    private readonly IMasterCompanyRepository _masterCompanyRepository;
    private readonly IMapper _mapper;
    private readonly IUserInformation _userInformation;

    public MasterCompanyService(IMasterCompanyRepository MasterCompanyRepository, IMapper mapper, IUserInformation userInformation)
    {
        _masterCompanyRepository = MasterCompanyRepository;
        _mapper = mapper;
        _userInformation = userInformation;
    }
    public async Task CreateAsync(MasterCompanyPost user)
    {
        await _masterCompanyRepository
            .CreateAsync(user.Name, user.Active, user.InitialUserEmail,
                user.InitialUserPassword);
    }

    public async Task<PagedResult<MasterCompanyGet>> GetPagedAsync(PagingModel pagingModel)
    {
        var result = await _masterCompanyRepository.GetPagedAsync(_userInformation.MasterCompanyId, pagingModel);

        return new PagedResult<MasterCompanyGet>(_mapper.Map<IEnumerable<MasterCompanyGet>>(result.Items),
            result.FilteredCount);

    }

    public async Task<MasterCompanyGet> FindByIdAsync(Guid MasterCompanyId)
    {
        var result = await _masterCompanyRepository.FindByIdAsync(MasterCompanyId);
        return _mapper.Map<MasterCompanyGet>(result);
    }

    public async Task UpdateAsync(MasterCompanyPut user)
    {
        await _masterCompanyRepository.UpdateAsync(user.MasterCompanyId, user.Name, user.Active);
    }
}
using Accounting.Common;
using Accounting.Core.Interfaces;
using Accounting.Core.Requests;
using Accounting.Infrastructure.Repositories.Interfaces;
using AutoMapper;

namespace Accounting.Core.Services;

public class SpecService : ISpecService
{
    private readonly ISpecRepository _specRepository;
    private readonly IUserInformation _userInformation;
    private readonly IMapper _mapper;

    public SpecService(ISpecRepository specRepository, IUserInformation userInformation, IMapper mapper)
    {
        _specRepository = specRepository;
        _userInformation = userInformation;
        _mapper = mapper;
    }
    public async Task<PagedResult<SpecGet>> GetPagedAsync(PagingModel pagingModel)
    {
        var result = await _specRepository.GetPagedAsync(_userInformation.MasterCompanyId, pagingModel);
        return new PagedResult<SpecGet>(_mapper.Map<IEnumerable<SpecGet>>(result.Items), result.FilteredCount);
    }
}
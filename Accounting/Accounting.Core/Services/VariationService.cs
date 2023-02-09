using Accounting.Common;
using Accounting.Core.Interfaces;
using Accounting.Core.Requests;
using Accounting.Infrastructure.Repositories.Interfaces;
using AutoMapper;

namespace Accounting.Core.Services;

public class VariationService : IVariationService
{
    private readonly IVariationRepository _variationRepository;
    private readonly IUserInformation _userInformation;
    private readonly IMapper _mapper;

    public VariationService(IVariationRepository variationRepository, IUserInformation userInformation, IMapper mapper)
    {
        _variationRepository = variationRepository;
        _userInformation = userInformation;
        _mapper = mapper;
    }

    public async Task<PagedResult<VariationGet>> GetPagedAsync(PagingModel pagingModel)
    {
        var result = await _variationRepository.GetPagedAsync(_userInformation.MasterCompanyId, pagingModel);
        return new PagedResult<VariationGet>(_mapper.Map<IEnumerable<VariationGet>>(result.Items), result.FilteredCount);
    }
}
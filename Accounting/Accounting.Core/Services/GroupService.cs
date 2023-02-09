using Accounting.Common;
using Accounting.Core.Interfaces;
using Accounting.Core.Requests;
using Accounting.Infrastructure.Models;
using Accounting.Infrastructure.Repositories.Interfaces;
using AutoMapper;

namespace Accounting.Core.Services;

public class GroupService : IGroupService
{
    private readonly IGroupRepository _groupRepository;
    private readonly IUserInformation _userInformation;
    private readonly IMapper _mapper;

    public GroupService(IGroupRepository groupRepository, IUserInformation userInformation, IMapper mapper)
    {
        _groupRepository = groupRepository;
        _userInformation = userInformation;
        _mapper = mapper;
    }

    public async Task<PagedResult<GroupGet>> GetPagedAsync(PagingModel pagingModel)
    {
        var result = await _groupRepository.GetPagedAsync(_userInformation.MasterCompanyId, pagingModel);
        return new PagedResult<GroupGet>(_mapper.Map<IEnumerable<GroupGet>>(result.Items), result.FilteredCount);
    }

    public async Task CreateAsync(GroupPost groupPost)
    {
        await _groupRepository.CreateAsync(_userInformation.MasterCompanyId, _mapper.Map<Group>(groupPost));
    }

    public async Task DeleteAsync(int groupId)
    {
        await _groupRepository.DeleteAsync(_userInformation.MasterCompanyId, groupId);
    }
}
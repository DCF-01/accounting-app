using Accounting.Common;
using Accounting.Core.Interfaces;
using Accounting.Core.Requests;
using Accounting.Infrastructure.Models;
using Accounting.Infrastructure.Repositories.Interfaces;
using AutoMapper;

namespace Accounting.Core.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly IUserInformation _userInformation;

    public UserService(IUserRepository userRepository, IMapper mapper, IUserInformation userInformation)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _userInformation = userInformation;
    }

    public async Task<PagedResult<UserGet>> GetPagedAsync(PagingModel pagingModel)
    {
        var result = await _userRepository.GetPagedAsync(_userInformation.MasterCompanyId, pagingModel);
        return new PagedResult<UserGet>(_mapper.Map<IEnumerable<UserGet>>(result.Items), result.FilteredCount);
    }

    public async Task UpdateAsync(UserPut userPut)
    {
        await _userRepository.UpdateAsync(_userInformation.MasterCompanyId, _mapper.Map<User>(userPut));
    }

    public async Task DeleteAsync(string userId)
    {
        await _userRepository.DeleteAsync(_userInformation.MasterCompanyId, userId);
    }

    public async Task CreateAsync(UserPost userPost)
    {
        await _userRepository
            .CreateAsync(
                _userInformation.MasterCompanyId, userPost.Password,
                _mapper.Map<User>(userPost)
            );
    }

    public async Task<UserGet> GetAsync(string userId)
    {
        return _mapper.Map<UserGet>(await _userRepository.GetAsync(_userInformation.MasterCompanyId, userId));
    }
}
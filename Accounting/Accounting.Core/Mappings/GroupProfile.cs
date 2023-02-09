using System.Text.RegularExpressions;
using Accounting.Core.Requests;
using AutoMapper;

namespace Accounting.Core.Mappings;

public class GroupProfile : Profile
{
    public GroupProfile()
    {
        CreateMap<GroupGet, Group>().ReverseMap();
        CreateMap<GroupPost, Group>();
    }
}
using AutoMapper;
using Notes.Application.Common.Mappings;
using UserService.Application.Queries.GetUserDetails;

namespace UserService.WebApi.Models;

public class GetUserDto : IMapWith<GetUserDetailsQuery>
{
    public Guid? Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? MiddleName { get; set; }
    public string? Phone { get; set; }
    public string? Email { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<GetUserDto, GetUserDetailsQuery>();
    }
}

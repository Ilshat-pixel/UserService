using AutoMapper;
using Notes.Application.Common.Mappings;
using UserService.Application.Commands.CreateUser;

namespace UserService.WebApi.Models
{
    public class CreateUserDto : IMapWith<CreateUserCommand>
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? MiddleName { get; set; }
        public DateOnly? BirthDay { get; set; }
        public string? PassportId { get; set; }
        public string? BirthPlace { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? RegistrationAddress { get; set; }
        public string? LivingAddress { get; set; }
        public string? Device { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateUserDto, CreateUserCommand>();
        }
    }
}

using AutoMapper;
using Notes.Application.Common.Mappings;
using UserService.Domain;

namespace UserService.Application.Queries.GetUserDetails
{
    public class UserDetailsVm : IMapWith<User>
    {
        public Guid Id { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? MiddleName { get; set; }

        public DateOnly? BirthDay { get; set; }

        public int? PassportId { get; set; }

        public string? BirthPlace { get; set; }

        public string? Phone { get; set; }

        public string? Email { get; set; }

        public string? RegistrationAddress { get; set; }

        public string? LivingAddress { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<User, UserDetailsVm>();
        }
    }
}

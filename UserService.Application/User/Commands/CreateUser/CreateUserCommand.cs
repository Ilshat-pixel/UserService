using MediatR;

namespace UserService.Application.Commands.CreateUser
{
    public class CreateUserCommand : IRequest<Guid>
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
    }
}

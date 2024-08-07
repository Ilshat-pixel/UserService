using MediatR;

namespace UserService.Application.Commands.DeleteCommand
{
    public class DeleteUserCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}

using MediatR;
using UserService.Application.Common.Exceptions;
using UserService.Application.Interfaces;
using UserService.Domain;

namespace UserService.Application.Commands.DeleteCommand
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand>
    {
        private readonly IUserDbContext _dbContext;

        public DeleteUserCommandHandler(IUserDbContext dbContext) => _dbContext = dbContext;

        public async Task Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.Users.FindAsync([request.Id], cancellationToken);

            if (entity == null)
            {
                throw new NotFoundException(nameof(User), request.Id);
            }

            _dbContext.Users.Remove(entity);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}

using MediatR;
using UserService.Application.Interfaces;
using UserService.Domain;

namespace UserService.Application.Commands.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Guid>
    {
        private readonly IUserDbContext _dbContext;

        public CreateUserCommandHandler(IUserDbContext dbContext) => _dbContext = dbContext;

        public async Task<Guid> Handle(
            CreateUserCommand request,
            CancellationToken cancellationToken
        )
        {
            var user = new User()
            {
                BirthDay = request.BirthDay,
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                LivingAddress = request.LivingAddress,
                MiddleName = request.MiddleName,
                PassportId = request.PassportId,
                BirthPlace = request.BirthPlace,
                Phone = request.Phone,
                RegistrationAddress = request.RegistrationAddress
            };

            await _dbContext.Users.AddAsync(user, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return user.Id;
        }
    }
}

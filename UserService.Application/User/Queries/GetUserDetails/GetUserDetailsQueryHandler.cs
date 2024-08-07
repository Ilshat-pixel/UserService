using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using UserService.Application.Interfaces;

namespace UserService.Application.Queries.GetUserDetails
{
    public class GetUserDetailsQueryHandler : IRequestHandler<GetUserDetailsQuery, UserDetailsVm>
    {
        private readonly IUserDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetUserDetailsQueryHandler(IUserDbContext dbContext, IMapper mapper) =>
            (_dbContext, _mapper) = (dbContext, mapper);

        public async Task<UserDetailsVm> Handle(
            GetUserDetailsQuery request,
            CancellationToken cancellationToken
        )
        {
            var userPredicate = _dbContext.Users.AsQueryable();

            if (request.Id != null && request.Id != Guid.Empty)
            {
                userPredicate = userPredicate.Where(x => x.Id == request.Id);
            }
            if (!string.IsNullOrEmpty(request.FirstName))
                userPredicate = userPredicate.Where(u =>
                    u.FirstName != null && u.FirstName.Contains(request.FirstName)
                );
            if (!string.IsNullOrEmpty(request.MiddleName))
                userPredicate = userPredicate.Where(u =>
                    u.MiddleName != null && u.MiddleName.Contains(request.MiddleName)
                );
            if (!string.IsNullOrEmpty(request.LastName))
                userPredicate = userPredicate.Where(u =>
                    u.LastName != null && u.LastName.Contains(request.LastName)
                );
            if (!string.IsNullOrEmpty(request.Phone))
                userPredicate = userPredicate.Where(u =>
                    u.Phone != null && u.Phone.Contains(request.Phone)
                );
            if (!string.IsNullOrEmpty(request.Email))
                userPredicate = userPredicate.Where(u =>
                    u.Email != null && u.Email.Contains(request.Email)
                );

            var user = await userPredicate.FirstOrDefaultAsync();
            return _mapper.Map<UserDetailsVm>(user);
        }
    }
}

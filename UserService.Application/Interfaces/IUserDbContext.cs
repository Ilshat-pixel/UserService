using Microsoft.EntityFrameworkCore;
using UserService.Domain;

namespace UserService.Application.Interfaces
{
    public interface IUserDbContext
    {
        public DbSet<User> Users { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}

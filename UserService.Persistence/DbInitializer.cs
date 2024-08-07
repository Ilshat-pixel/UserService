namespace UserService.Persistence
{
    public class DbInitializer
    {
        public static void Initialize(UsersDbContext context)
        {
            context.Database.EnsureCreated();
        }
    }
}

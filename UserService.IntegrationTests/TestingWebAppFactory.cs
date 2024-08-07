using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using UserService.Persistence;
using UserService.WebApi;

namespace UserService.IntegrationTests;

public class TestingWebAppFactory<TEntryPoint> : WebApplicationFactory<Program>
    where TEntryPoint : Program
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            var descriptor = services.SingleOrDefault(d =>
                d.ServiceType == typeof(DbContextOptions<UsersDbContext>)
            );
            if (descriptor != null)
            {
                services.Remove(descriptor);
            }

            services.AddDbContext<UsersDbContext>(options =>
            {
                options.UseInMemoryDatabase("TestDatabase");
            });
            var sp = services.BuildServiceProvider();
            using (var scope = sp.CreateScope())
            using (var appContext = scope.ServiceProvider.GetRequiredService<UsersDbContext>())
            {
                try
                {
                    appContext.Database.EnsureCreated();
                }
                catch (Exception ex)
                {
                    //Log errors or do anything you think it's needed
                    throw;
                }
            }
        });
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Database
{
    public static class DatabaseConfig
    {
        public static void Setup(IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddDbContext<DatabaseContext>(options =>
                {
                    options.UseSqlServer(configuration.GetConnectionString("Database"));
                });
        }
    }
}
using InvestCarControl.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace InvestCarControl.Configuration
{
    public static class DbContextConfig
    {
        public static IServiceCollection AddDbContextConfig(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<IdentyDbContext>(options =>
                options.UseMySql(configuration.GetConnectionString("IdentyDbContext"), builder =>
                builder.MigrationsAssembly("InvestCarControl")));
            return services;
        }
    }
}
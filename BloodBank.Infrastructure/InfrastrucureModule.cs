using BloodBank.Core.Repositories;
using BloodBank.Infrastructure.Persistence;
using BloodBank.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BloodBank.Infrastructure
{
    public static class InfrastrucureModule
    {
        public static IServiceCollection AddInfrastrucure(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddRepositories()
                .AddData(configuration);

            return services;
        }

        private static IServiceCollection AddData(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("BloodBankDB");
            services.AddDbContext<BloodBankDbContext>(options => options.UseSqlServer(connectionString));

            return services;
        }

        private static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IDoadorRepository, DoadorRepository>();
            services.AddScoped<IDoacaoRepository, DoacaoRepository>();
            return services;
        }
    }
}

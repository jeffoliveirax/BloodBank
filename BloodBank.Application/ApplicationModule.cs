using BloodBank.Application.Commands.InsertDoador;
using Microsoft.Extensions.DependencyInjection;

namespace BloodBank.Application
{
    public static class ApplicationModule
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services
                .AddHandlers();

            return services;
        }

        private static IServiceCollection AddHandlers(this IServiceCollection services)
        {
            services.AddMediatR(config => 
                config.RegisterServicesFromAssemblyContaining<InsertDoadorCommand>());

            return services;
        }
    }
}

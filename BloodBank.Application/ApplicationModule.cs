using BloodBank.Application.Commands.InsertDoador;
using BloodBank.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace BloodBank.Application
{
    public static class ApplicationModule
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services
                .AddServices()
                .AddHandlers();

            return services;
        }

        private static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IDoacaoService, DoacaoService>();
            services.AddScoped<IDoadorService, DoadorService>();
            services.AddScoped<IEstoqueService, EstoqueService>();

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

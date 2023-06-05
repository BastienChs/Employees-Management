using Application.Abstractions;
using Infrastructure.Common;
using Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services, 
            ConfigurationManager configuration)
        {
            services.Configure<EntrepriseDatabaseSettings>(configuration.GetSection(EntrepriseDatabaseSettings.SectionName));
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            return services;
        }
    }
}

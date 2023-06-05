using Application.Employees.Queries;
using AutoMapper;

namespace webapi.Common.Mapping
{
    public static class CustomAutoMapper
    {
        public static void AddCustomConfiguredAutoMapper(this IServiceCollection services)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AutoMapperProfileConfiguration>();
            });

            var mapper = config.CreateMapper();
            services.AddSingleton(mapper);
        }
    }
}

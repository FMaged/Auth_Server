
using Domain.Interfaces.Services;
using infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
             
            //AddSingleton =Create one single instance of HashService and reuse it everywhere.
            services.AddSingleton<IHashService, HashService>();  
            return services;
        }



    }
}

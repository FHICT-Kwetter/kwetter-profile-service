using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ProfileService.Service.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddServiceLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMediatR(typeof(ServiceCollectionExtensions));
        }
    }
}
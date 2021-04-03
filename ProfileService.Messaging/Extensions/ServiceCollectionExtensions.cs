namespace ProfileService.Messaging.Extensions
{
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using ProfileService.Messaging.Configuration;
    using ProfileService.Messaging.KubeMq;

    public static class ServiceCollectionExtensions
    {
        public static void AddKubeMqMessaging(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddOptions<KubeMqOptions>().Bind(configuration.GetSection("KubeMQ"));
            services.AddSingleton<IKubeMqServer, KubeMqServer>();
        }
    }
}
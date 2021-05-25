// <copyright file="Startup.cs" company="Kwetter">
//     Copyright Kwetter. All rights reserved.
// </copyright>
// <author>Dirk Heijnen</author>

using MediatR;

namespace ProfileService.Api
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using ProfileService.Api.Extensions;
    using ProfileService.Data.Extensions;
    using ProfileService.PubSub;
    using ProfileService.Service.Extensions;

    /// <summary>
    /// Defines the <see cref="Startup" />.
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// The configuration
        /// </summary>
        private readonly IConfiguration configuration;

        /// <summary>
        /// Initializes a new instance of the <see cref="Startup"/> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        /// <summary>
        /// Configures the services.
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services">The services <see cref="IServiceCollection" />.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApiLayer(this.configuration);
            services.AddServiceLayer(this.configuration);
            services.AddDatabaseLayer(this.configuration);
        }

        /// <summary>
        /// Configures the specified application.
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="env">The environment.</param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceScopeFactory scopeFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseCors(x => x.WithOrigins("http://localhost:3000").AllowAnyHeader().AllowAnyMethod());
            }

            app.UsePathBase(new PathString("/api/profile"));
            app.UseHealthChecks("/api/profile/health");
            app.UseHealthChecks("/health");
            app.UseRouting();
            app.UseCors(x =>
                x.WithOrigins("https://kwetter.org", "https://www.kwetter.org").AllowAnyHeader().AllowAnyMethod());
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

            PubSubService.Subscribe("profileservice--user-created", scopeFactory);
            PubSubService.Subscribe("profileservice--user-deleted", scopeFactory);
        }
    }
}
using System;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

namespace Phx.Gateway
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add authentication
            services
                .AddAuthentication("ADB2C")
                .AddAzureADB2CBearer(
                    "ADB2C",
                    "Jwt",
                    options => { Configuration.Bind("AzureAd", options); });

            const string applicationPolicy = "PhxAppAuth";
            services.AddAuthorization(options =>
            {
                // Add application authorization policy
                options.AddPolicy(applicationPolicy, policy =>
                {
                    policy.RequireAuthenticatedUser();
                });
            });

            // Add Ocelot integration to allow for authentication checks
            services.AddOcelot();

            services.AddMvc(options =>
            {
                // Add application policy as default for all requests
                options.Filters.Add(new AuthorizeFilter(applicationPolicy));
                options.AllowCombiningAuthorizeFilters = false;

            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            // Disable CORS until further notice
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", p =>
                {
                    p.AllowCredentials()
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowAnyOrigin()
                        .AllowCredentials()
                        .SetPreflightMaxAge(TimeSpan.FromDays(7))
                        .Build();
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app
                .UseHsts()
                .UseHttpsRedirection()
                .UseAuthentication()
                .UseDeveloperExceptionPage();

            app
                .UseCors("AllowAll")
                .UseMvc();

            app
                .UseOcelot()
                .Wait();
        }
    }
}

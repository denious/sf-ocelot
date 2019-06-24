using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Logging;

namespace Phx.Public.API
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
            services.AddSingleton(Configuration);

            Config.NETCore.PhxNetCoreConfig.Services = services;
            Config.NETCore.PhxNetCoreConfig.AddSnapshotCollector();
            Config.NETCore.PhxNetCoreConfig.AddCors();
            Config.NETCore.PhxNetCoreConfig.AddApi<Startup>();
            Config.NETCore.PhxNetCoreConfig.AddMvc(true);

            IdentityModelEventSource.ShowPII = true;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IApiVersionDescriptionProvider provider)
        {
            Phx.Config.NETCore.PhxNetCoreConfig.Configure(app, provider);
        }
    }
}

using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNet.OData.Query;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Phx.People.API
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
            Config.NETCore.PhxNetCoreConfig.AddMvc(false);

            // add standalone odata support
            services.AddOData();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IApiVersionDescriptionProvider provider)
        {
            Config.NETCore.PhxNetCoreConfig.Configure(app, provider, configureRoutes: builder =>
             {
                 // add standalone odata support
                 builder.EnableDependencyInjection();

                 builder
                     .Select()
                     .Filter()
                     .Count()
                     .OrderBy();
             });
        }
    }
}

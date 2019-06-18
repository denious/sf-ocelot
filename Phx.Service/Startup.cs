using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Phx.Service
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
            //Config.NETCore.PhxNetCoreConfig.AddSnapshotCollector();
            Config.NETCore.PhxNetCoreConfig.AddCors();
            Config.NETCore.PhxNetCoreConfig.AddApi<Startup>();
            Config.NETCore.PhxNetCoreConfig.AddOData();
            Config.NETCore.PhxNetCoreConfig.AddMvc(false);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env,
            VersionedODataModelBuilder modelBuilder, IApiVersionDescriptionProvider provider)
        {
            Phx.Config.NETCore.PhxNetCoreConfig.Configure(app, env, modelBuilder, provider);
        }
    }
}

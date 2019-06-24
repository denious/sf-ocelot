using System;
using System.Collections.Generic;
using System.Fabric;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.ServiceFabric.Services.Communication.AspNetCore;
using Microsoft.ServiceFabric.Services.Communication.Runtime;
using Microsoft.ServiceFabric.Services.Runtime;
using Serilog;
using Serilog.Core.Enrichers;
using ILogger = Serilog.ILogger;

namespace Phx.People.API
{
    /// <summary>
    /// The FabricRuntime creates an instance of this class for each service type instance. 
    /// </summary>
    internal sealed class API : StatelessService
    {
        public ILogger<API> Logger { get; }

        public API(StatelessServiceContext context, ILogger logger)
            : base(context)
        {
            PropertyEnricher[] properties = new PropertyEnricher[]
            {
                new PropertyEnricher("ServiceTypeName", context.ServiceTypeName),
                new PropertyEnricher("ServiceName", context.ServiceName),
                new PropertyEnricher("PartitionId", context.PartitionId),
                new PropertyEnricher("InstanceId", context.ReplicaOrInstanceId),
            };

            logger.ForContext(properties);

            Logger = new LoggerFactory().AddSerilog(logger.ForContext(properties)).CreateLogger<API>();
        }

        /// <summary>
        /// Optional override to create listeners (like tcp, http) for this service instance.
        /// </summary>
        /// <returns>The collection of listeners.</returns>
        protected override IEnumerable<ServiceInstanceListener> CreateServiceInstanceListeners()
        {
            return new ServiceInstanceListener[]
            {
                new ServiceInstanceListener(serviceContext =>
                    new KestrelCommunicationListener(serviceContext, "ServiceEndpoint", (url, listener) =>
                    {
                        ServiceEventSource.Current.ServiceMessage(serviceContext, $"Starting Kestrel on {url}");

                        return new WebHostBuilder()
                            .UseKestrel()
                            .UseContentRoot(AppContext.BaseDirectory)
                            .ConfigureAppConfiguration((hostingContext, config) =>
                            {
                                config
                                    .SetBasePath(hostingContext.HostingEnvironment.ContentRootPath)
                                    .AddJsonFile("phxappsettings.json", false, true)
                                    .AddJsonFile(
                                        $"phxappsettings.{hostingContext.HostingEnvironment.EnvironmentName}.json",
                                        true, true)
                                    .AddJsonFile("appsettings.json", false, true)
                                    .AddJsonFile(
                                        $"appsettings.{hostingContext.HostingEnvironment.EnvironmentName}.json", true,
                                        true)
                                    .AddEnvironmentVariables();
                            })
                            .ConfigureServices(services => services.AddSingleton(serviceContext))
                            .UseStartup<Startup>()
                            .UseServiceFabricIntegration(listener, ServiceFabricIntegrationOptions.UseUniqueServiceUrl)
                            .UseUrls(url)
                            .UseSerilog()
                            .UseApplicationInsights()
                            .Build();
                    }))
            };
        }
    }
}

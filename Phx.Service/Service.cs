using System.Collections.Generic;
using System.Fabric;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.ServiceFabric.Services.Communication.AspNetCore;
using Microsoft.ServiceFabric.Services.Communication.Runtime;
using Microsoft.ServiceFabric.Services.Runtime;
using Serilog;
using Serilog.Core.Enrichers;
using ILogger = Serilog.ILogger;

    namespace Phx.Service
    {
        /// <summary>
        /// The FabricRuntime creates an instance of this class for each service type instance. 
        /// </summary>
        internal sealed class Service : StatelessService
        {
            public ILogger<Service> Logger { get; }

            public Service(StatelessServiceContext context, ILogger logger)
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

                Logger = new LoggerFactory().AddSerilog(logger.ForContext(properties)).CreateLogger<Service>();
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
                                .ConfigureServices(
                                    services => services
                                        .AddSingleton<StatelessServiceContext>(serviceContext))
                                .UseContentRoot(Directory.GetCurrentDirectory())
                                .UseStartup<Startup>()
                                .UseServiceFabricIntegration(listener,
                                    ServiceFabricIntegrationOptions.UseUniqueServiceUrl)
                                .UseUrls(url)
                                .UseSerilog()
                                //.UseApplicationInsights()
                                .Build();
                        }))
                };
            }
        }
    }

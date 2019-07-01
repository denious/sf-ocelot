using System;
using System.Diagnostics;
using System.Threading;
using Autofac;
using Autofac.Integration.ServiceFabric;
using Microsoft.Extensions.Configuration;
using Phx.People.Data;

namespace Phx.People.v1_0.Business
{
    internal static class Program
    {
        /// <summary>
        /// This is the entry point of the service host process.
        /// </summary>
        private static void Main()
        {
            try
            {
                // The ServiceManifest.XML file defines one or more service type names.
                // Registering a service maps a service type name to a .NET type.
                // When Service Fabric creates an instance of this service type,
                // an instance of the class is created in this host process.

                // Start with the trusty old container builder.
                var builder = new ContainerBuilder();

                // Build configuration
                var config = (IConfiguration)new ConfigurationBuilder()
                    .AddEnvironmentVariables()
                    .Build();

                // Register configuration
                builder.RegisterInstance(config)
                    .As<IConfiguration>()
                    .SingleInstance();

                // Register DB context
                builder.RegisterType<PeopleContext>();

                // Register the Autofac magic for Service Fabric support.
                builder.RegisterServiceFabricSupport();

                // Register a stateless service...
                builder.RegisterStatelessService<Business>("Phx.People.v1_0.BusinessType");

                using (builder.Build())
                {
                    ServiceEventSource.Current.ServiceTypeRegistered(
                        Process.GetCurrentProcess().Id,
                        typeof(Business).Name);

                    // Prevents this host process from terminating so services keep running.
                    Thread.Sleep(Timeout.Infinite);
                }
            }
            catch (Exception e)
            {
                ServiceEventSource.Current.ServiceHostInitializationFailed(e.ToString());
                throw;
            }
        }
    }
}

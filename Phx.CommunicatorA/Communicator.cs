using System;
using System.Collections.Generic;
using System.Fabric;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Services.Communication.Runtime;
using Microsoft.ServiceFabric.Services.Remoting.Client;
using Microsoft.ServiceFabric.Services.Remoting.Runtime;
using Microsoft.ServiceFabric.Services.Runtime;
using Phx.Dependent;

namespace Phx.Communicator
{
    public static class CommunicatorInfo
    {
        public static string Uri => $"{Environment.GetEnvironmentVariable("Fabric_ApplicationName")}/{nameof(Phx)}.{nameof(Phx.Communicator)}";
    }

    /// <summary>
    /// An instance of this class is created for each service instance by the Service Fabric runtime.
    /// </summary>
    internal sealed class Communicator : StatelessService, ICommunicatorClient
    {
        public Communicator(StatelessServiceContext context)
            : base(context)
        { }

        protected override IEnumerable<ServiceInstanceListener> CreateServiceInstanceListeners()
        {
            return this.CreateServiceRemotingInstanceListeners();
        }

        public async Task<string> Hello()
        {
            var dependentClient = ServiceProxy.Create<IDependentClient>(new Uri(DependentInfo.Uri));
            var time = await dependentClient.GetUtcTime();

            return $"Hello from Communicator! My dependent tells me it is now {time.ToString("T")} UTC.";
        }
    }
}

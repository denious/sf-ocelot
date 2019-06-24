using System;
using System.Collections.Generic;
using System.Fabric;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Services.Communication.Runtime;
using Microsoft.ServiceFabric.Services.Remoting.Runtime;
using Microsoft.ServiceFabric.Services.Runtime;

namespace Phx.Dependent
{
    public static class DependentInfo
    {
        public static string Uri => $"{Environment.GetEnvironmentVariable("Fabric_ApplicationName")}/{nameof(Phx)}.{nameof(Phx.Dependent)}";
    }

    /// <summary>
    /// An instance of this class is created for each service instance by the Service Fabric runtime.
    /// </summary>
    internal sealed class Dependent : StatelessService, IDependentClient
    {
        public Dependent(StatelessServiceContext context)
            : base(context)
        { }

        protected override IEnumerable<ServiceInstanceListener> CreateServiceInstanceListeners()
        {
            return this.CreateServiceRemotingInstanceListeners();
        }

        public async Task<DateTimeOffset> GetUtcTime()
        {
            return DateTimeOffset.UtcNow;
        }
    }
}

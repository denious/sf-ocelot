using System;
using System.Collections.Generic;
using System.Fabric;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Services.Communication.Runtime;
using Microsoft.ServiceFabric.Services.Remoting.Runtime;
using Microsoft.ServiceFabric.Services.Runtime;

namespace Phx.Cars.v1_0.Business
{
    public static class BusinessService
    {
        public static Uri Uri => new Uri($"{Environment.GetEnvironmentVariable("Fabric_ApplicationName")}/Phx.Cars.v1_0.Business");
    }

    /// <summary>
    /// An instance of this class is created for each service instance by the Service Fabric runtime.
    /// </summary>
    internal sealed class Business : StatelessService, IBusinessClient
    {
        public Business(StatelessServiceContext context)
            : base(context)
        { }

        protected override IEnumerable<ServiceInstanceListener> CreateServiceInstanceListeners()
        {
            return this.CreateServiceRemotingInstanceListeners();
        }

        public async Task<string> Hello()
        {
            throw new System.NotImplementedException();
        }
    }
}

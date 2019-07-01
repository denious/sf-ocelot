using System;
using System.Collections.Generic;
using System.Fabric;
using System.Threading.Tasks;
using Autofac.Features.OwnedInstances;
using Microsoft.EntityFrameworkCore;
using Microsoft.ServiceFabric.Services.Communication.Runtime;
using Microsoft.ServiceFabric.Services.Remoting.Runtime;
using Microsoft.ServiceFabric.Services.Runtime;
using Phx.People.Data;

namespace Phx.People.v1_0.Business
{
    public static class BusinessService
    {
        public static Uri Uri => new Uri($"{Environment.GetEnvironmentVariable("Fabric_ApplicationName")}/Phx.People.v1_0.Business");
    }

    /// <summary>
    /// An instance of this class is created for each service instance by the Service Fabric runtime.
    /// </summary>
    public class Business : StatelessService, IBusinessClient
    {
        private readonly Func<Owned<PeopleContext>> _peopleCtx;

        public Business(StatelessServiceContext context, Func<Owned<PeopleContext>> peopleCtx)
            : base(context)
        {
            _peopleCtx = peopleCtx;
        }

        protected override IEnumerable<ServiceInstanceListener> CreateServiceInstanceListeners()
        {
            return this.CreateServiceRemotingInstanceListeners();
        }

        public async Task<List<Person>> AllPeople()
        {
            using (var ctx = _peopleCtx().Value)
            {
                return await ctx.People.ToListAsync();
            }
        }
    }
}

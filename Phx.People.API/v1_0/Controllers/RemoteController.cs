using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.ServiceFabric.Services.Remoting.Client;
using Phx.People.Data;
using Phx.People.v1_0.Business;

namespace Phx.People.API.v1_0.Controllers
{
    [ApiController]
    [Route("v{version:apiVersion}/[controller]")]
    public class RemoteController : ControllerBase
    {
        private readonly IBusinessClient _peopleClient;

        public RemoteController()
        {
            _peopleClient = ServiceProxy.Create<IBusinessClient>(BusinessService.Uri);
        }

        [HttpGet(nameof(GetConfiguration))]
        public async Task<IEnumerable<IConfigurationSection>> GetConfiguration()
        {
            var configurationSections = await _peopleClient.GetConfiguration();
            return configurationSections;
        }

        [HttpGet(nameof(People))]
        [EnableQuery]
        public async Task<IQueryable<Person>> People()
        {
            var people = await _peopleClient.AllPeople();
            return people.AsQueryable();
        }
    }
}

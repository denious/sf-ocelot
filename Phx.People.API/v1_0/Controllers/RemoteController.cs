using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using Microsoft.ServiceFabric.Services.Remoting.Client;
using Phx.People.Data;
using Phx.People.v1_0.Business;

namespace Phx.People.API.v1_0.Controllers
{
    [ApiController]
    [Route("v{version:apiVersion}/[controller]")]
    public class RemoteController : ControllerBase
    {
        [HttpGet]
        [EnableQuery]
        public async Task<IQueryable<Person>> AllPeople()
        {
            var communicatorClient = ServiceProxy.Create<IBusinessClient>(BusinessService.Uri);
            var people = await communicatorClient.AllPeople();

            return people.AsQueryable();
        }
    }
}

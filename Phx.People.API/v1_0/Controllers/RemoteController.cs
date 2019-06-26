using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Query;
using Microsoft.AspNetCore.Identity.UI.V4.Pages.Account.Manage.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.ServiceFabric.Services.Remoting.Client;
using Phx.People.v1_0.Business;

namespace Phx.People.API.v1_0.Controllers
{
    [ApiController]
    [Route("v{version:apiVersion}/[controller]")]
    public class RemoteController : ControllerBase
    {
        [HttpPost(nameof(CallCommunicator))]
        public async Task<ActionResult<string>> CallCommunicator()
        {
            var communicatorClient = ServiceProxy.Create<IBusinessClient>(BusinessService.Uri);
            var name = await communicatorClient.Hello();

            return name;
        }

        [HttpGet]
        [EnableQuery]
        public async Task<IQueryable<Person>> People(ODataQueryOptions<Person> options)
        {
            var people = new List<Person>
            {
                new Person
                {
                    Name = "Denis"
                },
                new Person
                {
                    Name = "Eric"
                }
            };

            var query = people.AsQueryable();
            options.ApplyTo(query);

            return query;
        }
    }

    public class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }
}

using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.ServiceFabric.Services.Remoting.Client;
using Phx.Cars.v1_0.Business;

namespace Phx.Cars.API.v1_0.Controllers
{
    [ApiController]
    [Route("v{version:apiVersion}/[controller]")]
    public class RemoteController : ControllerBase
    {
        [HttpGet(nameof(CallCommunicator))]
        public async Task<ActionResult<string>> CallCommunicator()
        {
            var communicatorClient = ServiceProxy.Create<IBusinessClient>(BusinessService.Uri);
            var name = await communicatorClient.Hello();

            return name;
        }
    }
}

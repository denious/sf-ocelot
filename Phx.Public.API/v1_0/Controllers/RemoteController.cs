using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.ServiceFabric.Services.Remoting.Client;
using Phx.Communicator;

namespace Phx.Public.API.v1_0.Controllers
{
    [ApiController]
    [Route("v{version:apiVersion}/[controller]")]
    public class RemoteController : ControllerBase
    {
        [HttpGet(nameof(CallCommunicator))]
        public async Task<ActionResult<string>> CallCommunicator()
        {
            var communicatorClient = ServiceProxy.Create<ICommunicatorClient>(new Uri(CommunicatorInfo.Uri));
            var name = await communicatorClient.Hello();

            return name;
        }
    }
}

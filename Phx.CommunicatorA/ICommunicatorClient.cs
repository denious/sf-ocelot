using System.Threading.Tasks;
using Microsoft.ServiceFabric.Services.Remoting;

namespace Phx.Communicator
{
    public interface ICommunicatorClient : IService
    {
        Task<string> Hello();
    }
}

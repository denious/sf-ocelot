using System.Threading.Tasks;
using Microsoft.ServiceFabric.Services.Remoting;

namespace Phx.Cars.Business
{
    public interface IBusinessClient : IService
    {
        Task<string> Hello();
    }
}

using System.Threading.Tasks;
using Microsoft.ServiceFabric.Services.Remoting;

namespace Phx.People.v1_0.Business
{
    public interface IBusinessClient : IService
    {
        Task<string> Hello();
    }
}

using System;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Services.Remoting;

namespace Phx.Dependent
{
    public interface IDependentClient : IService
    {
        Task<DateTimeOffset> GetUtcTime();
    }
}

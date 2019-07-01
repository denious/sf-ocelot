using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.ServiceFabric.Services.Remoting;
using Phx.People.Data;

namespace Phx.People.v1_0.Business
{
    public interface IBusinessClient : IService
    {
        Task<IEnumerable<IConfigurationSection>> GetConfiguration();
        Task<List<Person>> AllPeople();
    }
}

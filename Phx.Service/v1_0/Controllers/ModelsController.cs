using System.Collections.Generic;
using System.Linq;
using System.Net;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Mvc;
using Phx.Service.v1_0.Models;

namespace Phx.Service.v1_0.Controllers
{
    [ODataRoutePrefix("Models")]
    public class ModelsController : ODataController
    {
        [ODataRoute]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ODataValue<IEnumerable<Model>>), (int)HttpStatusCode.OK)]
        [EnableQuery]
        public IQueryable<Model> Get()
        {
            var models = new List<Model>
            {
                new Model
                {
                    Id = 1,
                    Name = "First"
                },
                new Model
                {
                    Id = 2,
                    Name = "Second"
                }
            };

            return models.AsQueryable();
        }
    }
}

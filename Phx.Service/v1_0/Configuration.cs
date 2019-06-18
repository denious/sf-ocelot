using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNetCore.Mvc;

namespace Phx.Service.v1_0
{
    public class Configuration : IModelConfiguration
    {
        private const int MajorVersion = 1;
        private const int MinorVersion = 0;

        public void Apply(ODataModelBuilder builder, ApiVersion apiVersion)
        {
            if (apiVersion.MajorVersion == MajorVersion && apiVersion.MinorVersion == MinorVersion)
                Configure(builder);
        }

        public static void Configure(ODataModelBuilder builder)
        {
            BuildObject(builder);
        }

        private static void BuildObject(ODataModelBuilder builder)
        {
            builder.EntitySet<Models.Model>("Models");
        }
    }
}

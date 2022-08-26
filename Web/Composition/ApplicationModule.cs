using Autofac;
using EonixWebApi.Infrastructure.Services;
using Microsoft.Extensions.Configuration;

namespace EonixWebApi.Web.Composition
{
    public class ApplicationModule : CompositionModule
    {
        public ApplicationModule(IConfiguration configuration) : base(configuration) {}
        protected override void Register(ContainerBuilder builder)
        {
            ScanAndRegister<ContactService>(builder);
        }

    }
}

using Autofac;
using WTService = WTBeiboot_SS21_Albus.Service;

namespace WTBeiboot_SS21_Albus
{
    public class MicroserviceContainerModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterModule<WTService.ContainerModule>();
        }
    }
}

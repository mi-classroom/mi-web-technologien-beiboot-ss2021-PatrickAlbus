using Autofac;
using WTBeiboot_SS21_Albus.Service.Services;
using WTBeiboot_SS21_Albus.Service.Contracts.Services;

namespace WTBeiboot_SS21_Albus.Service
{
    public class ContainerModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterType<DirectoryService>()
                .As<IDirectoryService>();
            builder
                .RegisterType<FileService>()
                .As<IFileService>();
        }
    }
}

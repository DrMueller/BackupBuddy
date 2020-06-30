using Mmu.Mlh.ConsoleExtensions.Areas.Commands.Services;
using Mmu.Mlh.ServiceProvisioning.Areas.Initialization.Models;
using Mmu.Mlh.ServiceProvisioning.Areas.Initialization.Services;

namespace Mmu.BackupBuddy.TestConsole
{
    public static class Program
    {
        public static void Main()
        {
            var containerConfig = ContainerConfiguration.CreateFromAssembly(typeof(Program).Assembly);
            var container = ServiceProvisioningInitializer.CreateContainer(containerConfig);
            container
                .GetInstance<IConsoleCommandsStartupService>()
                .Start();
        }
    }
}
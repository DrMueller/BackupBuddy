using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Mmu.Mlh.ConsoleExtensions.Areas.Commands.Services;
using Mmu.Mlh.ServiceProvisioning.Areas.Initialization.Models;
using Mmu.Mlh.ServiceProvisioning.Areas.Initialization.Services;

namespace Mmu.BackupBuddy.TestConsole
{
    public class Program
    {
        public static void Main()
        {
            var serviceDescriptors = new List<ServiceDescriptor>();

            using (var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole()))
            {
                var logger = loggerFactory.CreateLogger<Program>();
                serviceDescriptors.Add(new ServiceDescriptor(typeof(ILogger), logger));
            }

            var containerConfig = ContainerConfiguration.CreateFromAssembly(typeof(Program).Assembly);
            var container = ServiceProvisioningInitializer.CreateContainer(containerConfig, serviceDescriptors);

            container
                .GetInstance<IConsoleCommandsStartupService>()
                .Start();
        }
    }
}
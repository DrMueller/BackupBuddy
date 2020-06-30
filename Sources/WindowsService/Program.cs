using Lamar;
using Lamar.Microsoft.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Mmu.Mlh.ServiceProvisioning.Areas.Initialization.Models;
using Mmu.Mlh.ServiceProvisioning.Areas.Initialization.Services;

namespace Mmu.BackupBuddy.WindowsService
{
    public static class Program
    {
        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            var containerConfig = ContainerConfiguration.CreateFromAssembly(typeof(Program).Assembly);

            return Host.CreateDefaultBuilder(args)
                .UseLamar()
                .UseWindowsService()
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHostedService<Worker>();
                })
                .ConfigureContainer<ServiceRegistry>((context, services) =>
                {
                    ServiceProvisioningInitializer.PopulateRegistry(containerConfig, services);
                });
        }

        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }
    }
}
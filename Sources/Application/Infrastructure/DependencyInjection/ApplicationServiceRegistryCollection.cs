using Lamar;
using Mmu.BackupBuddy.Application.Infrastructure.Settings.Services;
using Mmu.BackupBuddy.Application.Infrastructure.Settings.Services.Implementation;

namespace Mmu.BackupBuddy.Application.Infrastructure.DependencyInjection
{
    public class ApplicationServiceRegistryCollection : ServiceRegistry
    {
        public ApplicationServiceRegistryCollection()
        {
            Scan(scanner =>
            {
                scanner.AssemblyContainingType<ApplicationServiceRegistryCollection>();
                scanner.WithDefaultConventions();
            });

            For<IAppSettingsProvider>().Use<AppSettingsProvider>().Singleton();
        }
    }
}
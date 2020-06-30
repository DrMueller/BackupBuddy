using Lamar;

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
        }
    }
}
using JetBrains.Annotations;
using Lamar;
using Mmu.Mlh.ConsoleExtensions.Areas.Commands.Models;

namespace Mmu.BackupBuddy.TestConsole.Infrastructure.DependencyInjection
{
    [PublicAPI]
    public class TestConsoleRegistryCollection : ServiceRegistry
    {
        public TestConsoleRegistryCollection()
        {
            Scan(
                scanner =>
                {
                    scanner.AssemblyContainingType<TestConsoleRegistryCollection>();
                    scanner.AddAllTypesOf<IConsoleCommand>();

                    scanner.WithDefaultConventions();
                });
        }
    }
}
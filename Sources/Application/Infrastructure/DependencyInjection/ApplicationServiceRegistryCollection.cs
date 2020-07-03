using JetBrains.Annotations;
using Lamar;
using Mmu.BackupBuddy.Application.Areas.Orchestration.Services;
using Mmu.BackupBuddy.Application.Areas.Orchestration.Services.Implementation;
using Mmu.BackupBuddy.Application.Areas.SubAreas.BackupCleaning.Services;
using Mmu.BackupBuddy.Application.Areas.SubAreas.BackupCleaning.Services.Implementation;
using Mmu.BackupBuddy.Application.Areas.SubAreas.FileSaving.Services;
using Mmu.BackupBuddy.Application.Areas.SubAreas.FileSaving.Services.Implementation;
using Mmu.BackupBuddy.Application.Areas.SubAreas.TempPath.Services;
using Mmu.BackupBuddy.Application.Areas.SubAreas.TempPath.Services.Implementation;
using Mmu.BackupBuddy.Application.Areas.SubAreas.Zipping.Services;
using Mmu.BackupBuddy.Application.Areas.SubAreas.Zipping.Services.Implementation;
using Mmu.BackupBuddy.Application.Infrastructure.Directories.Services;
using Mmu.BackupBuddy.Application.Infrastructure.Directories.Services.Implementation;
using Mmu.BackupBuddy.Application.Infrastructure.Settings.Services;
using Mmu.BackupBuddy.Application.Infrastructure.Settings.Services.Implementation;

namespace Mmu.BackupBuddy.Application.Infrastructure.DependencyInjection
{
    [PublicAPI]
    public class ApplicationServiceRegistryCollection : ServiceRegistry
    {
        public ApplicationServiceRegistryCollection()
        {
            For<IBackupOrchestrationService>().Use<BackupOrchestrationService>().Singleton();
            For<IBackupCleaner>().Use<BackupCleaner>().Singleton();
            For<IZipFileSaver>().Use<ZipFileSaver>().Singleton();
            For<IZipFileFactory>().Use<ZipFileFactory>().Singleton();
            For<IAppSettingsProvider>().Use<AppSettingsProvider>().Singleton();
            For<ITempPathRepository>().Use<TempPathRepository>().Singleton();
            For<IDirectoryRepository>().Use<DirectoryRepository>().Singleton();
        }
    }
}
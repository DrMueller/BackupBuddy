using Microsoft.Extensions.Logging;
using Mmu.BackupBuddy.Application.Areas.SubAreas.BackupCleaning.Services;
using Mmu.BackupBuddy.Application.Areas.SubAreas.FileSaving.Services;
using Mmu.BackupBuddy.Application.Areas.SubAreas.Zipping.Services;
using Mmu.BackupBuddy.Application.Infrastructure.Settings.Services;

namespace Mmu.BackupBuddy.Application.Areas.Orchestration.Services.Implementation
{
    public class BackupOrchestrationService : IBackupOrchestrationService
    {
        private readonly IAppSettingsProvider _appSetingsProvider;
        private readonly IBackupCleaner _backupCleaner;
        private readonly IZipFileFactory _zipFileFactory;
        private readonly IZipFileSaver _zipFileSaver;

        public BackupOrchestrationService(
            IZipFileFactory zipFileFactory,
            IZipFileSaver zipFileSaver,
            IBackupCleaner backupCleaner,
            IAppSettingsProvider appSetingsProvider)
        {
            _zipFileFactory = zipFileFactory;
            _zipFileSaver = zipFileSaver;
            _backupCleaner = backupCleaner;
            _appSetingsProvider = appSetingsProvider;
        }

        public void CreateBackups(ILogger logger)
        {
            var settings = _appSetingsProvider.ProvideAppSettings();

            logger.LogInformation($"TargetBaseDirectory: {settings.TargetBaseDirectory}");
            logger.LogInformation($"Cnt: {settings.BackupSettings.Count}");

            var zipFiles = _zipFileFactory.CreateZipFiles();
            _zipFileSaver.SaveZipFiles(zipFiles);
            _backupCleaner.CleanOldBackups();
        }
    }
}
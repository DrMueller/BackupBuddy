using Mmu.BackupBuddy.Application.Areas.SubAreas.BackupCleaning.Services;
using Mmu.BackupBuddy.Application.Areas.SubAreas.FileSaving.Services;
using Mmu.BackupBuddy.Application.Areas.SubAreas.Zipping.Services;

namespace Mmu.BackupBuddy.Application.Areas.Orchestration.Services.Implementation
{
    public class BackupOrchestrationService : IBackupOrchestrationService
    {
        private readonly IBackupCleaner _backupCleaner;
        private readonly IZipFileFactory _zipFileFactory;
        private readonly IZipFileSaver _zipFileSaver;

        public BackupOrchestrationService(
            IZipFileFactory zipFileFactory,
            IZipFileSaver zipFileSaver,
            IBackupCleaner backupCleaner)
        {
            _zipFileFactory = zipFileFactory;
            _zipFileSaver = zipFileSaver;
            _backupCleaner = backupCleaner;
        }

        public void CreateBackups()
        {
            var zipFiles = _zipFileFactory.CreateZipFiles();
            _zipFileSaver.SaveZipFiles(zipFiles);
            _backupCleaner.CleanOldBackups();
        }
    }
}
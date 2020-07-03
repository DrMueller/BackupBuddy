using Mmu.BackupBuddy.Application.Areas.SubAreas.BackupCleaning.Services;
using Mmu.BackupBuddy.Application.Areas.SubAreas.FileSaving.Services;
using Mmu.BackupBuddy.Application.Areas.SubAreas.TempPath.Services;
using Mmu.BackupBuddy.Application.Areas.SubAreas.Zipping.Services;

namespace Mmu.BackupBuddy.Application.Areas.Orchestration.Services.Implementation
{
    public class BackupOrchestrationService : IBackupOrchestrationService
    {
        private readonly IBackupCleaner _backupCleaner;
        private readonly ITempPathRepository _tempPathRepository;
        private readonly IZipFileFactory _zipFileFactory;
        private readonly IZipFileSaver _zipFileSaver;

        public BackupOrchestrationService(
            IZipFileFactory zipFileFactory,
            IZipFileSaver zipFileSaver,
            IBackupCleaner backupCleaner,
            ITempPathRepository tempPathRepository)
        {
            _zipFileFactory = zipFileFactory;
            _zipFileSaver = zipFileSaver;
            _backupCleaner = backupCleaner;
            _tempPathRepository = tempPathRepository;
        }

        public void CreateBackups()
        {
            try
            {
                _tempPathRepository.InitializeTempPath();
                var zipFiles = _zipFileFactory.CreateZipFiles();
                _zipFileSaver.SaveZipFiles(zipFiles);
                _backupCleaner.CleanOldBackups();
            }
            finally
            {
                _tempPathRepository.ClearTempPath();
            }
        }
    }
}
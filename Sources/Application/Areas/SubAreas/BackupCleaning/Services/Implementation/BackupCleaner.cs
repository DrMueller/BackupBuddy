using System.IO.Abstractions;
using System.Linq;
using Mmu.BackupBuddy.Application.Infrastructure.Settings.Services;
using Mmu.Mlh.LanguageExtensions.Areas.Collections;

namespace Mmu.BackupBuddy.Application.Areas.SubAreas.BackupCleaning.Services.Implementation
{
    public class BackupCleaner : IBackupCleaner
    {
        private readonly IAppSettingsProvider _appSettingsProvider;
        private readonly IFileSystem _fileSystem;

        public BackupCleaner(
            IAppSettingsProvider appSettingsProvider,
            IFileSystem fileSystem)
        {
            _appSettingsProvider = appSettingsProvider;
            _fileSystem = fileSystem;
        }

        public void CleanOldBackups()
        {
            var appSettings = _appSettingsProvider.ProvideAppSettings();
            var directoriesToCheck = appSettings
                .BackupSettings
                .Select(bs => _fileSystem.Path.Combine(appSettings.TargetBaseDirectory, bs.TargetSubDirectory))
                .ToList();

            directoriesToCheck.ForEach(CheckCleanDirectory);
        }

        private void CheckCleanDirectory(string directory)
        {
            var directoryInfo = _fileSystem.DirectoryInfo.FromDirectoryName(directory);

            directoryInfo.EnumerateFiles()
                .OrderByDescending(f => f.CreationTime)
                .Skip(5)
                .ForEach(f => f.Delete());
        }
    }
}
using System.Collections.Generic;
using System.IO.Abstractions;
using Mmu.BackupBuddy.Application.Areas.SubAreas.Zipping.Models;
using Mmu.BackupBuddy.Application.Infrastructure.Directories.Services;
using Mmu.BackupBuddy.Application.Infrastructure.Settings.Services;
using Mmu.Mlh.LanguageExtensions.Areas.Collections;

namespace Mmu.BackupBuddy.Application.Areas.SubAreas.FileSaving.Services.Implementation
{
    internal class ZipFileSaver : IZipFileSaver
    {
        private readonly IAppSettingsProvider _appSettingsProvider;
        private readonly IDirectoryRepository _directoryRepo;
        private readonly IFileSystem _fileSystem;

        public ZipFileSaver(
            IAppSettingsProvider appSettingsProvider,
            IDirectoryRepository directoryRepo,
            IFileSystem fileSystem)
        {
            _appSettingsProvider = appSettingsProvider;
            _directoryRepo = directoryRepo;
            _fileSystem = fileSystem;
        }

        public void SaveZipFiles(IReadOnlyCollection<ZipFile> zipFiles)
        {
            var targetBaseDirectory = _appSettingsProvider.ProvideAppSettings().TargetBaseDirectory;
            _directoryRepo.AssureDirectoryExists(targetBaseDirectory);

            zipFiles.ForEach(zf => SaveZipFile(zf, targetBaseDirectory));
        }

        private void SaveZipFile(ZipFile zipFile, string targetBaseDirectory)
        {
            var targetPath = _fileSystem.Path.Combine(targetBaseDirectory, zipFile.TargetSubDirectory);
            _directoryRepo.AssureDirectoryExists(targetPath);

            var fileName = _fileSystem.Path.GetFileName(zipFile.TemporaryFilePath);
            var targetFilePath = _fileSystem.Path.Combine(targetPath, fileName);

            _fileSystem.File.Copy(zipFile.TemporaryFilePath, targetFilePath, true);
        }
    }
}
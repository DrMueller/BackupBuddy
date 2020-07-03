using System;
using System.Collections.Generic;
using System.IO.Abstractions;
using System.IO.Compression;
using System.Linq;
using Mmu.BackupBuddy.Application.Areas.SubAreas.TempPath.Services;
using Mmu.BackupBuddy.Application.Infrastructure.Directories.Services;
using Mmu.BackupBuddy.Application.Infrastructure.Settings.Models;
using Mmu.BackupBuddy.Application.Infrastructure.Settings.Services;

namespace Mmu.BackupBuddy.Application.Areas.SubAreas.Zipping.Services.Implementation
{
    internal class ZipFileFactory : IZipFileFactory
    {
        private readonly IAppSettingsProvider _appSettingsProvider;
        private readonly IDirectoryRepository _directoryRepo;
        private readonly IFileSystem _fileSystem;
        private readonly ITempPathRepository _tempPathRepository;

        public ZipFileFactory(
            IAppSettingsProvider appSettingsProvider,
            IDirectoryRepository directoryRepo,
            IFileSystem fileSystem,
            ITempPathRepository tempPathRepository)
        {
            _appSettingsProvider = appSettingsProvider;
            _directoryRepo = directoryRepo;
            _fileSystem = fileSystem;
            _tempPathRepository = tempPathRepository;
        }

        public IReadOnlyCollection<Models.ZipFile> CreateZipFiles()
        {
            var tempPath = _tempPathRepository.GetTempPath();
            var backupSettings = _appSettingsProvider.ProvideAppSettings().BackupSettings;
            var zipFiles = backupSettings.Select(bs => CreateZipFile(bs, tempPath)).ToList();

            return zipFiles;
        }

        private Models.ZipFile CreateZipFile(BackupSetting backupSetting, string tempPath)
        {
            var currentDate = DateTime.Now.ToString("yyyyMMddHHmmss");
            var tempSubPath = _fileSystem.Path.Combine(tempPath, backupSetting.TargetSubDirectory);
            _directoryRepo.AssureDirectoryExists(tempSubPath);
            var tempFilePath = _fileSystem.Path.Combine(tempSubPath, currentDate);
            tempFilePath = _fileSystem.Path.ChangeExtension(tempFilePath, "zip");

            ZipFile.CreateFromDirectory(backupSetting.DirectoryToBackup, tempFilePath, CompressionLevel.Optimal, false);

            return new Models.ZipFile(tempFilePath, backupSetting.TargetSubDirectory);
        }
    }
}
using System;
using System.Collections.Generic;
using System.IO.Abstractions;
using System.IO.Compression;
using System.Linq;
using Mmu.BackupBuddy.Application.Infrastructure.Directories.Services;
using Mmu.BackupBuddy.Application.Infrastructure.Settings.Models;
using Mmu.BackupBuddy.Application.Infrastructure.Settings.Services;
using ZipFile = Mmu.BackupBuddy.Application.Areas.SubAreas.Zipping.Models.ZipFile;

namespace Mmu.BackupBuddy.Application.Areas.SubAreas.Zipping.Services.Implementation
{
    public class ZipFileFactory : IZipFileFactory
    {
        private readonly IAppSettingsProvider _appSettingsProvider;
        private readonly IDirectoryRepository _directoryRepo;
        private readonly IFileSystem _fileSystem;

        public ZipFileFactory(
            IAppSettingsProvider appSettingsProvider,
            IDirectoryRepository directoryRepo,
            IFileSystem fileSystem)
        {
            _appSettingsProvider = appSettingsProvider;
            _directoryRepo = directoryRepo;
            _fileSystem = fileSystem;
        }

        public IReadOnlyCollection<ZipFile> CreateZipFiles()
        {
            var tempPath = _fileSystem.Path.GetTempPath();
            var backupSettings = _appSettingsProvider.ProvideAppSettings().BackupSettings;
            var zipFiles = backupSettings.Select(bs => CreateZipFile(bs, tempPath)).ToList();

            return zipFiles;
        }

        private ZipFile CreateZipFile(BackupSetting backupSetting, string tempPath)
        {
            var currentDate = DateTime.Now.ToString("yyyyMMddHHmmss");
            var tempSubPath = _fileSystem.Path.Combine(tempPath, backupSetting.TargetSubDirectory);
            _directoryRepo.AssureDirectoryExists(tempSubPath);
            var tempFilePath = _fileSystem.Path.Combine(tempSubPath, currentDate);
            tempFilePath = _fileSystem.Path.ChangeExtension(tempFilePath, "zip");

            System.IO.Compression.ZipFile.CreateFromDirectory(backupSetting.DirectoryToBackup, tempFilePath, CompressionLevel.Optimal, false);

            return new ZipFile(tempFilePath, backupSetting.TargetSubDirectory);
        }
    }
}
using System;
using System.IO.Abstractions;
using System.Linq;
using Mmu.BackupBuddy.Application.Infrastructure.Settings.Dtos;
using Mmu.BackupBuddy.Application.Infrastructure.Settings.Models;
using Mmu.Mlh.ApplicationExtensions.Areas.Dropbox.Services;
using Mmu.Mlh.SettingsProvisioning.Areas.Factories;
using Mmu.Mlh.SettingsProvisioning.Areas.Models;

namespace Mmu.BackupBuddy.Application.Infrastructure.Settings.Services.Implementation
{
    internal class AppSettingsProvider : IAppSettingsProvider
    {
        private readonly IDropboxLocator _dropboxLocator;
        private readonly IFileSystem _fileSystem;
        private readonly Lazy<AppSettings> _lazySettings;
        private readonly ISettingsFactory _settingsFactory;

        public AppSettingsProvider(
            IFileSystem fileSystem,
            ISettingsFactory settingsFactory,
            IDropboxLocator dropboxLocator)
        {
            _fileSystem = fileSystem;
            _settingsFactory = settingsFactory;
            _dropboxLocator = dropboxLocator;
            _lazySettings = new Lazy<AppSettings>(CreateAppSettings);
        }

        public AppSettings ProvideAppSettings()
        {
            return _lazySettings.Value;
        }

        private AppSettings CreateAppSettings()
        {
            var settingsConfig = new SettingsConfiguration(
                AppSettings.SectionKey,
                string.Empty,
                string.Empty,
                @"Apps\BackupBuddy\");

            var dropboxPath = _dropboxLocator
                .LocateDropboxPath()
                .Reduce(() => throw new NotSupportedException("Couldn't find Dropbox path"));

            var settingsDto = _settingsFactory.CreateSettings<AppSettingsDto>(settingsConfig);
            var backupSettings = settingsDto.BackupSettings.Select(dto => CreateBackupSetting(dropboxPath, dto)).ToList();

            return new AppSettings(settingsDto.TargetBaseDirectory, backupSettings);
        }

        private BackupSetting CreateBackupSetting(string dropboxPath, BackupSettingDto dto)
        {
            var absoluteDirectoryToBackup = _fileSystem.Path.Combine(dropboxPath, dto.RelativeDropboxPathToBackup);

            return new BackupSetting(absoluteDirectoryToBackup, dto.TargetSubDirectory);
        }
    }
}
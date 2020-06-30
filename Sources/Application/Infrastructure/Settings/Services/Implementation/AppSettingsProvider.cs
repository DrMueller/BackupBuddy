using System;
using System.Linq;
using Mmu.BackupBuddy.Application.Infrastructure.Settings.Dtos;
using Mmu.BackupBuddy.Application.Infrastructure.Settings.Models;
using Mmu.Mlh.SettingsProvisioning.Areas.Factories;
using Mmu.Mlh.SettingsProvisioning.Areas.Models;

namespace Mmu.BackupBuddy.Application.Infrastructure.Settings.Services.Implementation
{
    public class AppSettingsProvider : IAppSettingsProvider
    {
        private readonly Lazy<AppSettings> _lazySettings;
        private readonly ISettingsFactory _settingsFactory;

        public AppSettingsProvider(ISettingsFactory settingsFactory)
        {
            _settingsFactory = settingsFactory;
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

            var settingsDto = _settingsFactory.CreateSettings<AppSettingsDto>(settingsConfig);
            var backupSettings = settingsDto.BackupSettings.Select(f => new BackupSetting(f.DirectoryToBackup, f.TargetSubDirectory)).ToList();

            return new AppSettings(settingsDto.TargetBaseDirectory, backupSettings);
        }
    }
}
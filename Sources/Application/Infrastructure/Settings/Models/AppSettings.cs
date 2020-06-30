using System.Collections.Generic;

namespace Mmu.BackupBuddy.Application.Infrastructure.Settings.Models
{
    public class AppSettings
    {
        public const string SectionKey = "AppSettings";

        public AppSettings(string targetBaseDirectory, IReadOnlyCollection<BackupSetting> backupSettings)
        {
            TargetBaseDirectory = targetBaseDirectory;
            BackupSettings = backupSettings;
        }

        public IReadOnlyCollection<BackupSetting> BackupSettings { get; }

        public string TargetBaseDirectory { get; }
    }
}
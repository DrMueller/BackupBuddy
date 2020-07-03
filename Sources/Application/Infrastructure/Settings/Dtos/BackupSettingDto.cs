using JetBrains.Annotations;

namespace Mmu.BackupBuddy.Application.Infrastructure.Settings.Dtos
{
    [PublicAPI]
    public class BackupSettingDto
    {
        public string RelativeDropboxPathToBackup { get; set; }
        public string TargetSubDirectory { get; set; }
    }
}
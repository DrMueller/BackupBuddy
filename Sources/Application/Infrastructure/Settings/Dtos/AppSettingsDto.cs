using System.Collections.Generic;

namespace Mmu.BackupBuddy.Application.Infrastructure.Settings.Dtos
{
    public class AppSettingsDto
    {
        public List<BackupSettingDto> BackupSettings { get; set; }
        public string TargetBaseDirectory { get; set; }
    }
}
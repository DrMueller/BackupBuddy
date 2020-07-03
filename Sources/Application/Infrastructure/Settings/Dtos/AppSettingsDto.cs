using System.Collections.Generic;
using JetBrains.Annotations;

namespace Mmu.BackupBuddy.Application.Infrastructure.Settings.Dtos
{
    [PublicAPI]
    public class AppSettingsDto
    {
        public List<BackupSettingDto> BackupSettings { get; set; }
        public string TargetBaseDirectory { get; set; }
    }
}
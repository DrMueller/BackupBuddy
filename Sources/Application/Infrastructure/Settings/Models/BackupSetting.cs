namespace Mmu.BackupBuddy.Application.Infrastructure.Settings.Models
{
    public class BackupSetting
    {
        public BackupSetting(string directoryToBackup, string targetSubDirectory)
        {
            DirectoryToBackup = directoryToBackup;
            TargetSubDirectory = targetSubDirectory;
        }

        public string DirectoryToBackup { get; }
        public string TargetSubDirectory { get; }
    }
}
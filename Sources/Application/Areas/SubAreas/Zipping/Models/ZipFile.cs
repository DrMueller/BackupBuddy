namespace Mmu.BackupBuddy.Application.Areas.SubAreas.Zipping.Models
{
    public class ZipFile
    {
        public ZipFile(string temporaryFilePath, string targetSubDirectory)
        {
            TemporaryFilePath = temporaryFilePath;
            TargetSubDirectory = targetSubDirectory;
        }

        public string TargetSubDirectory { get; }

        public string TemporaryFilePath { get; }
    }
}
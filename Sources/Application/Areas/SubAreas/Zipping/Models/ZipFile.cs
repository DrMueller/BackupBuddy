using Mmu.Mlh.LanguageExtensions.Areas.Invariance;

namespace Mmu.BackupBuddy.Application.Areas.SubAreas.Zipping.Models
{
    public class ZipFile
    {
        public ZipFile(string temporaryFilePath, string targetSubDirectory)
        {
            Guard.StringNotNullOrEmpty(() => temporaryFilePath);
            Guard.StringNotNullOrEmpty(() => targetSubDirectory);

            TemporaryFilePath = temporaryFilePath;
            TargetSubDirectory = targetSubDirectory;
        }

        public string TargetSubDirectory { get; }

        public string TemporaryFilePath { get; }
    }
}
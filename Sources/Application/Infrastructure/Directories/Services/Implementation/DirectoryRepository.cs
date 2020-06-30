using System.IO.Abstractions;
using Mmu.Mlh.LanguageExtensions.Areas.Collections;

namespace Mmu.BackupBuddy.Application.Infrastructure.Directories.Services.Implementation
{
    public class DirectoryRepository : IDirectoryRepository
    {
        private readonly IFileSystem _fileSystem;

        public DirectoryRepository(IFileSystem fileSystem)
        {
            _fileSystem = fileSystem;
        }

        public void AssureDirectoryExists(string directory)
        {
            if (!_fileSystem.Directory.Exists(directory))
            {
                _fileSystem.Directory.CreateDirectory(directory);
            }
        }

        public void CleanDirectoryRecursive(string directory)
        {
            var directoryInfo = _fileSystem.DirectoryInfo.FromDirectoryName(directory);
            RecursiveDelete(directoryInfo);
        }

        private static void RecursiveDelete(IDirectoryInfo directoryInfo)
        {
            if (!directoryInfo.Exists)
            {
                return;
            }

            directoryInfo.EnumerateDirectories().ForEach(RecursiveDelete);
            directoryInfo.Delete(true);
        }
    }
}
using System.IO.Abstractions;

namespace Mmu.BackupBuddy.Application.Infrastructure.Directories.Services.Implementation
{
    internal class DirectoryRepository : IDirectoryRepository
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
    }
}
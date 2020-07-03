using System;
using System.IO.Abstractions;
using Mmu.BackupBuddy.Application.Infrastructure.Directories.Services;
using Mmu.Mlh.LanguageExtensions.Areas.Collections;

namespace Mmu.BackupBuddy.Application.Areas.SubAreas.TempPath.Services.Implementation
{
    internal class TempPathRepository : ITempPathRepository
    {
        private readonly IDirectoryRepository _directoryRepo;
        private readonly IFileSystem _fileSystem;
        private string _tempPath;

        public TempPathRepository(IFileSystem fileSystem, IDirectoryRepository directoryRepo)
        {
            _fileSystem = fileSystem;
            _directoryRepo = directoryRepo;
        }

        public void ClearTempPath()
        {
            if (string.IsNullOrEmpty(_tempPath))
            {
                return;
            }

            var directoryInfo = _fileSystem.DirectoryInfo.FromDirectoryName(_tempPath);
            RecursiveDelete(directoryInfo);
        }

        public string GetTempPath()
        {
            return _tempPath;
        }

        public void InitializeTempPath()
        {
            var baseTempPath = _fileSystem.Path.GetTempPath();
            _tempPath = _fileSystem.Path.Combine(baseTempPath, Guid.NewGuid().ToString());
            _directoryRepo.AssureDirectoryExists(_tempPath);
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
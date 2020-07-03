using System.Collections.Generic;
using Mmu.BackupBuddy.Application.Areas.SubAreas.Zipping.Models;

namespace Mmu.BackupBuddy.Application.Areas.SubAreas.Zipping.Services
{
    public interface IZipFileFactory
    {
        IReadOnlyCollection<ZipFile> CreateZipFiles();
    }
}
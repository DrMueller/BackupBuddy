using System.Collections.Generic;
using Mmu.BackupBuddy.Application.Areas.SubAreas.Zipping.Models;

namespace Mmu.BackupBuddy.Application.Areas.SubAreas.FileSaving.Services
{
    public interface IZipFileSaver
    {
        void SaveZipFiles(IReadOnlyCollection<ZipFile> zipFiles);
    }
}
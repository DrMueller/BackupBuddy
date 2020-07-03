namespace Mmu.BackupBuddy.Application.Areas.SubAreas.TempPath.Services
{
    public interface ITempPathRepository
    {
        void ClearTempPath();

        string GetTempPath();

        void InitializeTempPath();
    }
}
namespace Mmu.BackupBuddy.Application.Infrastructure.Directories.Services
{
    public interface IDirectoryRepository
    {
        void AssureDirectoryExists(string directory);
    }
}
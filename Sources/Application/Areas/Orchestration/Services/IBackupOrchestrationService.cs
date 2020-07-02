using Microsoft.Extensions.Logging;

namespace Mmu.BackupBuddy.Application.Areas.Orchestration.Services
{
    public interface IBackupOrchestrationService
    {
        void CreateBackups(ILogger logger);
    }
}
using System;
using System.Threading.Tasks;
using Mmu.BackupBuddy.Application.Areas.Orchestration.Services;
using Mmu.Mlh.ConsoleExtensions.Areas.Commands.Models;

namespace Mmu.BackupBuddy.TestConsole.ConsoleCommands
{
    public class CreateBackups : IConsoleCommand
    {
        private readonly IBackupOrchestrationService _backupOrchestrator;

        public CreateBackups(IBackupOrchestrationService backupOrchestrator)
        {
            _backupOrchestrator = backupOrchestrator;
        }

        public string Description { get; } = "Create Backups";
        public ConsoleKey Key { get; } = ConsoleKey.F1;

        public Task ExecuteAsync()
        {
            _backupOrchestrator.CreateBackups();

            return Task.CompletedTask;
        }
    }
}
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Mmu.BackupBuddy.Application.Areas.Orchestration.Services;
using Mmu.Mlh.ConsoleExtensions.Areas.Commands.Models;

namespace Mmu.BackupBuddy.TestConsole.Areas.ConsoleCommands
{
    public class CreateBackups : IConsoleCommand
    {
        private readonly IBackupOrchestrationService _backupOrchestrator;
        private readonly ILogger _logger;

        public CreateBackups(IBackupOrchestrationService backupOrchestrator, ILogger logger)
        {
            _backupOrchestrator = backupOrchestrator;
            _logger = logger;
        }

        public string Description { get; } = "Create Backups";
        public ConsoleKey Key { get; } = ConsoleKey.F1;

        public Task ExecuteAsync()
        {
            _backupOrchestrator.CreateBackups(_logger);

            return Task.CompletedTask;
        }
    }
}
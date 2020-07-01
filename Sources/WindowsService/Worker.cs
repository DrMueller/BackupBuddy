using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Mmu.BackupBuddy.Application.Areas.Orchestration.Services;
using Timer = System.Timers.Timer;

namespace Mmu.BackupBuddy.WindowsService
{
    public class Worker : BackgroundService
    {
        private readonly IBackupOrchestrationService _backupOrchestrator;
        private readonly ILogger<Worker> _logger;
        private CancellationToken _stoppingToken;
        private Timer _timer;

        public Worker(ILogger<Worker> logger, IBackupOrchestrationService backupOrchestrator)
        {
            _logger = logger;
            _backupOrchestrator = backupOrchestrator;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _stoppingToken = stoppingToken;
            _timer = new Timer(1000 * 60 * 60 * 12);
            _timer.Elapsed += Timer_Elapsed;

            _logger.LogInformation("Starting backup...");
            _backupOrchestrator.CreateBackups();

            return Task.CompletedTask;
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (_stoppingToken.IsCancellationRequested)
            {
                _timer.Stop();

                return;
            }

            _logger.LogInformation("Starting backup...");
            _backupOrchestrator.CreateBackups();
        }
    }
}
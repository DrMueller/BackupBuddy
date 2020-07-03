using System;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Mmu.BackupBuddy.Application.Areas.Orchestration.Services;

namespace Mmu.BackupBuddy.WindowsService
{
    public class Worker : BackgroundService
    {
        private const int IntervalInMilliSeconds = 1000 * 60 * 60 * 4;
        private readonly IBackupOrchestrationService _backupOrchestrator;
        private readonly ILogger<Worker> _logger;
        private CancellationToken _stoppingToken;
        private System.Timers.Timer _timer;

        public Worker(ILogger<Worker> logger, IBackupOrchestrationService backupOrchestrator)
        {
            _logger = logger;
            _backupOrchestrator = backupOrchestrator;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _stoppingToken = stoppingToken;
            _timer = new System.Timers.Timer(IntervalInMilliSeconds);
            _timer.Elapsed += Timer_Elapsed;

            CreateBackups();

            return Task.CompletedTask;
        }

        private void CreateBackups()
        {
            try
            {
                _logger.LogInformation("Starting backup...");
                _backupOrchestrator.CreateBackups();
                _logger.LogInformation("Backup finished..");
                _logger.LogInformation($"Next backup schedules in around {new TimeSpan(0, 0, 0, 0, IntervalInMilliSeconds).Hours} hours..");
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, ex.Message);
            }
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (_stoppingToken.IsCancellationRequested)
            {
                _timer.Stop();

                return;
            }

            CreateBackups();
        }
    }
}
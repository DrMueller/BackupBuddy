using Mmu.BackupBuddy.Application.Infrastructure.Settings.Models;

namespace Mmu.BackupBuddy.Application.Infrastructure.Settings.Services
{
    public interface IAppSettingsProvider
    {
        AppSettings ProvideAppSettings();
    }
}
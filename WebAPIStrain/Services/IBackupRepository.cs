using System.Threading.Tasks;

namespace WebAPIStrain.Services
{
    public interface IBackupRepository
    {
        Task<byte[]> BackupDatabaseAsync();
        Task RestoreDatabaseAsync(string backupFilePath);
    }
}
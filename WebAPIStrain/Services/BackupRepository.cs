using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using WebAPIStrain.Entities;

namespace WebAPIStrain.Services
{
    public class BackupRepository : IBackupRepository
    {
        private readonly IrtContext _context;
        private readonly IConfiguration _configuration;

        public BackupRepository(IrtContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<byte[]> BackupDatabaseAsync()
        {
            string backupFolderPath = _configuration["BackupFolderPath"];
            if (!Directory.Exists(backupFolderPath))
            {
                Directory.CreateDirectory(backupFolderPath);
            }

            string backupFileName = $"backup_{DateTime.Now:yyyyMMddHHmmss}.bak";
            string backupFilePath = Path.Combine(backupFolderPath, backupFileName);
            string backupQuery = $"BACKUP DATABASE [{_context.Database.GetDbConnection().Database}] TO DISK = '{backupFilePath}'";

            await _context.Database.ExecuteSqlRawAsync(backupQuery);

            byte[] fileBytes = await File.ReadAllBytesAsync(backupFilePath);
            return fileBytes;
        }

        public async Task RestoreDatabaseAsync(string backupFilePath)
        {
            try
            {
                await _context.Database.ExecuteSqlRawAsync("USE master; ALTER DATABASE IRT SET SINGLE_USER WITH ROLLBACK IMMEDIATE;");
                string restoreQuery = $"RESTORE DATABASE IRT FROM DISK = '{backupFilePath}' WITH REPLACE";
                await _context.Database.ExecuteSqlRawAsync(restoreQuery);
                await _context.Database.ExecuteSqlRawAsync("ALTER DATABASE IRT SET MULTI_USER;");
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to restore database: {ex.Message}");
            }
        }

    }
}

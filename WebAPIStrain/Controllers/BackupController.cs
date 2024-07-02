using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using WebAPIStrain.Services;

namespace WebAPIStrain.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BackupController : ControllerBase
    {
        private readonly IBackupRepository _backupRepository;

        public BackupController(IBackupRepository backupRepository)
        {
            _backupRepository = backupRepository;
        }

        [HttpGet("backup")]
        public async Task<IActionResult> BackupDatabase()
        {
            try
            {
                byte[] backupData = await _backupRepository.BackupDatabaseAsync();
                return File(backupData, "application/octet-stream", $"backup_{DateTime.Now:yyyyMMddHHmmss}.bak");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost("restore/{backupFilePath}")]
        public async Task<IActionResult> RestoreDatabase(string backupFilePath)
        {
            if (string.IsNullOrWhiteSpace(backupFilePath) || !System.IO.File.Exists(backupFilePath))
            {
                return BadRequest("Backup file path is not provided or the file does not exist.");
            }

            try
            {
                string backupFolderPath = Path.GetDirectoryName(backupFilePath);
                if (!Directory.Exists(backupFolderPath))
                {
                    Directory.CreateDirectory(backupFolderPath);
                }

                await _backupRepository.RestoreDatabaseAsync(backupFilePath);

                return Ok("Database restored successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

    }
}

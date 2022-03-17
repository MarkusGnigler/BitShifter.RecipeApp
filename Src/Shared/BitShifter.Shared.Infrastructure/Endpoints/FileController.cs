using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BitShifter.Shared.Kernel.Endpoints;
using BitShifter.Shared.Abstractions.Interfaces;

namespace BitShifter.Shared.Infrastructure.Endpoints
{
    public class FileController : BaseApiController
    {
        private readonly IWebRootWatcher _fileWatcher;

        public FileController(IWebRootWatcher fileWatcher)
        {
            _fileWatcher = fileWatcher;
        }

        [HttpGet("{filename}")]
        public async Task<IActionResult> DownloadFile(string filename)
        {
            if (string.IsNullOrEmpty(filename)) return BadRequest($"Requested filename is empty");

            var fileStream = await _fileWatcher.GetFileStreamAsync(filename);

            var fileStreamResult = new FileStreamResult(
                fileStream, $"image/{Path.GetExtension(filename).Replace(".", "")}");

            return fileStreamResult;
        }

        [HttpPost]
        public async Task<IActionResult> UploadFile([FromForm] IFormFile file)
        {
            if (file is null) return BadRequest("No File was loaded");
            if (file.Length == 0) return BadRequest("No File was loaded");
            if (string.IsNullOrEmpty(file.FileName)) return BadRequest($"Requested filename is empty");

            await _fileWatcher.SaveFileStreamAsync(file.FileName,
                async x => await file.CopyToAsync(x));

            return Created($"/file/{file.FileName}", file);
        }

        [HttpDelete("{filename}")]
        public async Task<IActionResult> Delete(string filename)
        {
            if (string.IsNullOrEmpty(filename)) return BadRequest($"Requested filename is empty");

            await _fileWatcher.RemoveFileAsync(filename);

            return Ok("Erfolgreich gelöscht");
        }

    }
}

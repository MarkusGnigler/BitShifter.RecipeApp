using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Hosting;
using PixelDance.Shared.Abstractions.Interfaces;
using Microsoft.Extensions.Logging;

namespace PixelDance.Shared.Infrastructure.Services
{
    internal class WebRootWatcher : IWebRootWatcher
    {
        private const string NOT_FOUND = "wwwroot/no-image.png";

        private readonly ICollection<FileInfo> _files;
        private readonly ILogger<WebRootWatcher> _logger;
        private readonly IFileService _fileService;

        public string WebRootPath { get; init; }

        public WebRootWatcher(
            ILogger<WebRootWatcher> logger, 
            IWebHostEnvironment environment, 
            IFileService fileService)
        {
            _logger = logger;
            _fileService = fileService;
            WebRootPath = Path.Combine(environment.WebRootPath, "images");

            Directory.CreateDirectory(WebRootPath);

            _files = _fileService
                .GetFilesWithinDirectories(WebRootPath)
                .ToList();

            _logger.LogInformation("Cached \"{count}\" files.", _files.Count());
        }

        private IEnumerable<FileInfo> GetFiles() => _files;
        private FileInfo GetFileByName(string fileName) => _files.FirstOrDefault(x => x.Name == fileName);

        private FileStream GetFileStream(string fileName)
        {
            var file = GetFileByName(fileName);
            try
            {
                return new FileStream(file?.FullName ?? NOT_FOUND, FileMode.Open, FileAccess.Read);
            }
            catch
            {
                return new FileStream(NOT_FOUND, FileMode.Open, FileAccess.Read);
            }
        }

        public Task<FileStream> GetFileStreamAsync(string fileName)
            => Task.Run(() => GetFileStream(fileName));

        private void SaveFileStream(string fileName, Action<FileStream> saveAction)
        {
            var path = Path.Combine(WebRootPath, fileName);

            if (File.Exists(path)) return;

            using var stream = new FileStream(path, FileMode.Create, FileAccess.Write);

            //await file.CopyToAsync(stream);
            saveAction(stream);

            _files.Add(new FileInfo(path));
        }

        public Task SaveFileStreamAsync(string fileName, Action<FileStream> saveAction)
            => Task.Run(() => SaveFileStream(fileName, saveAction));

        private void RemoveFile(string fileName)
        {
            var file = GetFileByName(fileName);
            if (file is null) return;
            if (!File.Exists(file.FullName)) return;
            if (file.Name == Path.GetFileName(NOT_FOUND)) return;

            File.Delete(file.FullName);
        }

        public Task RemoveFileAsync(string fileName)
            => Task.Run(() => RemoveFile(fileName));

    }
}

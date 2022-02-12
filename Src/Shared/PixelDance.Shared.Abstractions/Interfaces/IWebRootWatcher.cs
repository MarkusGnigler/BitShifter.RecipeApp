using System;
using System.IO;
using System.Threading.Tasks;

namespace PixelDance.Shared.Abstractions.Interfaces
{
    public interface IWebRootWatcher
    {
        string WebRootPath { get; init; }

        Task<FileStream> GetFileStreamAsync(string filename);
        Task SaveFileStreamAsync(string fileName, Action<FileStream> saveAction);
        Task RemoveFileAsync(string fileName);
    }
}

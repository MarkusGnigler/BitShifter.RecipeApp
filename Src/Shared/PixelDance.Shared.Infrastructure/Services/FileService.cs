using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using PixelDance.Shared.Abstractions.Interfaces;

namespace PixelDance.Shared.Infrastructure.Services
{
    internal class FileService : IFileService
    {
        public bool CopyFileIfNotExists(FileInfo sourceFile, Func<FileInfo, string> destinationFile)
        {
            var destinationFileName = destinationFile(sourceFile);

            if (File.Exists(destinationFileName)) return false;

            CreateDirectoryIfNotExists(Path.GetDirectoryName(destinationFileName));

            File.Copy(sourceFile.FullName, destinationFileName);

            return true;
        }

        public IEnumerable<FileInfo> GetFilesWithinDirectories(string directoryPath, IEnumerable<string> matchFunction = default)
        {
            List<FileInfo> fileInfos = new();
            if (matchFunction is null) matchFunction = new[] { "" };

            var subdirectoriesFiles = GetDirectoriesFromPath(directoryPath,
                    x => matchFunction.Any(id => x.Name.Contains(id)))
                .SelectMany(x => this.GetFilesWithinDirectories(x.FullName, matchFunction));

            fileInfos.AddRange(subdirectoriesFiles);

            var files = GetFilesFromDirectory(directoryPath,
                x => matchFunction.Any(id => x.Name.Contains(id)));

            fileInfos.AddRange(files);

            return fileInfos;
        }

        public IEnumerable<FileInfo> GetFilesFromDirectory(string directoryPath, Func<FileInfo, bool> matchFunction = default)
        {
            if (matchFunction is default(Func<FileInfo, bool>))
                matchFunction = x => true;

            var files = Directory.EnumerateFiles(directoryPath)
                .Select(x => new FileInfo(x))
                .Where(matchFunction)
                .ToArray();

            return files;
        }

        public IEnumerable<DirectoryInfo> GetDirectoriesFromPath(string directoryPath, Func<DirectoryInfo, bool> matchFunction = default)
        {
            if (matchFunction is default(Func<DirectoryInfo, bool>))
                matchFunction = x => true;

            var directories = Directory.EnumerateDirectories(directoryPath)
                .Select(x => new DirectoryInfo(x))
                .Where(matchFunction)
                .ToArray();

            return directories;
        }

        #region [ Private ]

        private void CreateDirectoryIfNotExists(string directoryPath)
        {
            if (Directory.Exists(directoryPath)) return;
            Directory.CreateDirectory(directoryPath);
        }

        #endregion

    }
}

using System;
using System.IO;
using System.Collections.Generic;

namespace BitShifter.Shared.Abstractions.Interfaces
{
    public interface IFileService
    {
        /// <summary>
        /// Copy the given file to the destinationfile from the destinationFile function
        /// </summary>
        /// <param name="sourceFile">File to copy</param>
        /// <param name="destinationFile">Function for parsing the destination path in dependency of the source file</param>
        /// <returns>Ture if copyed | False if not</returns>
        bool CopyFileIfNotExists(FileInfo sourceFile, Func<FileInfo, string> destinationFile);
        /// <summary>
        /// Load all FileInfos from all files with subdirectories with a possible filter criteria
        /// </summary>
        /// <param name="directoryPath"></param>
        /// <param name="matchFunction"></param>
        /// <returns></returns>
        IEnumerable<FileInfo> GetFilesWithinDirectories(string directoryPath, IEnumerable<string>? matchFunction = default);
        /// <summary>
        /// Load all FileInfos from a directory path with a possible filter criteria
        /// </summary>
        /// <param name="directoryPath">directory path</param>
        /// <param name="matchFunction">Filter criteria</param>
        /// <returns>All loaded FileInfos</returns>
        IEnumerable<FileInfo> GetFilesFromDirectory(string directoryPath, Func<FileInfo, bool>? matchFunction = default);
        /// <summary>
        /// Load all DirectoryInfos from a directory path with a possible filter criteria
        /// </summary>
        /// <param name="directoryPath">directory path</param>
        /// <param name="matchFunction">Filter criteria</param>
        /// <returns>All loaded DirectoryInfos</returns>
        IEnumerable<DirectoryInfo> GetDirectoriesFromPath(string directoryPath, Func<DirectoryInfo, bool>? matchFunction = default);
    }
}

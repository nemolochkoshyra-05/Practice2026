using System.Collections.Generic;
using System.IO;
using CommandLib;

namespace FileSystemCommands
{
    public class FindFilesCommand : ICommand
    {
        private readonly string _directoryPath;
        private readonly string _searchPattern;
        public List<string> FoundFiles { get; private set; } = new List<string>();

        public FindFilesCommand(string directoryPath, string searchPattern)
        {
            _directoryPath = directoryPath;
            _searchPattern = searchPattern;
        }

        public void Execute()
        {
            if (Directory.Exists(_directoryPath))
            {
                FoundFiles = new List<string>(Directory.GetFiles(_directoryPath, _searchPattern, SearchOption.AllDirectories));
            }
        }
    }
}
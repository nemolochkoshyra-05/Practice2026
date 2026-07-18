using System.IO;
using System.Linq;
using CommandLib;

namespace FileSystemCommands
{
    public class DirectorySizeCommand : ICommand
    {
        private readonly string _directoryPath;

        public long TotalSize { get; private set; }

        public DirectorySizeCommand(string directoryPath)
        {
            _directoryPath = directoryPath;
        }

        public void Execute()
        {
            if (Directory.Exists(_directoryPath))
            {
                TotalSize = Directory.GetFiles(_directoryPath, "*", SearchOption.AllDirectories)
                                     .Sum(file => new FileInfo(file).Length);
            }
        }
    }
}
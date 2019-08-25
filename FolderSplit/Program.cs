using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Security.AccessControl;

namespace FolderSplit
{
    class Program
    {
        private static readonly string SourcePath = $"{Directory.GetCurrentDirectory()}\\Seun";
        private static readonly string DestinationPath = $"{Directory.GetCurrentDirectory()}\\Result";
        private static int _count;
        private static int _nameCounter;
        private static readonly string directoryName = "Folder";
      
        static void Main(string[] args)
        {
            var files = Directory.EnumerateFiles(SourcePath);
            var filesNames = new List<string>();

            foreach (var file in files)
            {
                filesNames.Add(file);
                _count += 1;

                if (_count % 2 == 0)
                {
                    _nameCounter += 1;
                    var directoryPath = Path.Combine(DestinationPath, $"{directoryName}{_nameCounter.ToString()}");
                    var directoryInfo = new DirectoryInfo(directoryPath);
                    directoryInfo.Create();
                   
                    foreach (var name in filesNames)
                    {
                        var fileInfo = new FileInfo(name);
                        var sourcePath = Path.Combine(SourcePath,fileInfo.Name);
                        var destinationPath = Path.Combine(directoryPath, fileInfo.Name);

                        File.Copy(sourcePath, destinationPath, true);

                        var fileDeleteEvent = new FileDeleteEvent();
                        fileDeleteEvent.DeleteCopiedFiles += FileDeleteEventOnDeleteCopiedFiles;
                        fileDeleteEvent.CleanUp(name);
                    }

                    _count = 0;
                    filesNames = new List<string>();

                }
            }

            Console.WriteLine("Done");
            Console.ReadLine();
        }

        private static void FileDeleteEventOnDeleteCopiedFiles(object sender, FileDeleteEventArgs e)
        {
            Console.WriteLine("Showing files being deleted.");
            File.Delete(e.FileName);
        }
    }
}

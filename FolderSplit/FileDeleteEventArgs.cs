using System;

namespace FolderSplit
{
    public class FileDeleteEventArgs : EventArgs
    {
        public string FileName;

        public FileDeleteEventArgs(string fileName)
        {
            FileName = fileName;
        }
    }
}
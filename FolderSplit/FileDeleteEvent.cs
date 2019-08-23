using System;

namespace FolderSplit
{
    public class FileDeleteEvent
    {
        public event EventHandler<FileDeleteEventArgs> DeleteCopiedFiles;

        public void CleanUp(string fileName)
        {
            OnDeleteCopiedFiles(new FileDeleteEventArgs(fileName));
        }

        protected virtual void OnDeleteCopiedFiles(FileDeleteEventArgs e)
        {
            DeleteCopiedFiles?.Invoke(this, e);
        }
    }
}
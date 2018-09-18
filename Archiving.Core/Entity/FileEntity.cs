using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Archiving.Entity
{
    public class FileEntity
    {
        private readonly string _fileName;
        public string FileName
        {
            get
            {
                ensureUnlocked();
                return _fileName;
            }
        }

        string _dirPath;
        public string DirectoryPath
        {
            get
            {
                ensureUnlocked();
                return _dirPath;
            }
        }

        DateTime _newTime;
        public DateTime NewTime
        {
            get
            {
                ensureUnlocked();
                return _newTime;
            }
        }

        public string FullPath
        {
            get
            {
                ensureUnlocked();
                return Path.Combine(DirectoryPath, FileName);
            }
        }

        public FileEntity(string fileName, string directoryPath, DateTime newTime)
        {
            if (string.IsNullOrWhiteSpace(fileName))
                throw new ArgumentException(nameof(fileName) + "is null or empty.");
            if (string.IsNullOrWhiteSpace(directoryPath))
                throw new ArgumentException(nameof(directoryPath) + "is null or empty.");
            if (newTime == null)
                throw new ArgumentNullException(nameof(newTime) + "is null.");

            _fileName = fileName;
            _dirPath = directoryPath;
            _newTime = newTime;
        }

        public FileEntity(string fullPath, DateTime newTime)
            : this(Path.GetFileName(fullPath), Path.GetDirectoryName(fullPath), newTime) { }

        object _lock = null;
        public void LockBy(object obj)
        {
            ensureUnlocked();
            _lock = obj;
        }

        public bool Locked => _lock != null;
        private void ensureUnlocked()
        {
            if (_lock != null)
                throw new AccessViolationException("entity is locked by other object.");
        }

        public static FileEntity Create(FileInfo info)
        {
            if (info == null) throw new ArgumentNullException(nameof(info) + "is null");

            return new FileEntity(info.Name, info.DirectoryName, info.LastWriteTime);
        }

        public static FileEntity Create(FileSystemEventArgs arg)
        {
            if (arg == null) throw new ArgumentNullException(nameof(arg) + "is null");

            return Create(new FileInfo(arg.FullPath));
        }
    }
}

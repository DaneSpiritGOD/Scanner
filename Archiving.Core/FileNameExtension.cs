using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace System.IO
{
    public static class FileNameExtension
    {
        public static string GetFileNameWithTimeStamp(this string fileName) =>
            $"{Path.GetFileNameWithoutExtension(fileName)}({TimeStamp.DetailNow}){Path.GetExtension(fileName)}";

        public static string GetFileName(this string fileName, bool addTimeStamp) =>
            addTimeStamp ? GetFileNameWithTimeStamp(fileName) : fileName;
    }
}

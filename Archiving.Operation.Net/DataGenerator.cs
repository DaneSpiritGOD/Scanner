using Archiving.Entity;
using Basket;
using Google.Protobuf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Archiving.Operation.Net
{
    internal class DataGenerator
    {
        public DataGenerator() { }

        public byte[] Gen(FileEntity entity)
        {
            return genCore(entity);
        }

        private byte[] genCore(FileEntity entity)
        {
            var ff = new ImageFile();
            ff.FileName = entity.FileName;
            ff.FileContent = ByteString.CopyFrom(File.ReadAllBytes(entity.FullPath));
            ff.FileTime = entity.NewTime.ToString();

            var mix = new Mix();
            mix.ImageFile = ff;
            return mix.ToByteArray();
        }
    }
}

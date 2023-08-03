using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Basecode.Services.Interfaces;

namespace Basecode.Services.Services
{
    /// <summary>
    /// service class for handling files
    /// </summary>
    public class FileService : IFileService
    {
        public void DeleteFile(string path)
        {
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
        }

        public void SaveFile(string path, byte[] data)
        {
            using (var stream = new FileStream(path, FileMode.CreateNew))
            {
                using (var writer = new BinaryWriter(stream))
                {
                    writer.Write(data, 0, data.Length);
                }
            }
        }
    }
}

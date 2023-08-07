using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Basecode.Services.Interfaces;

namespace Basecode.Services.Services
{
    public class FileService : IFileService
    {
        /// <summary>
        /// Deletes a file from the specified path if it exists.
        /// </summary>
        /// <param name="path">The full path of the file to be deleted.</param>
        public void DeleteFile(string path)
        {
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
        }

        /// <summary>
        /// Saves binary data to a file at the specified path.
        /// </summary>
        /// <param name="path">The full path of the file to be created and saved.</param>
        /// <param name="data">The binary data to be written to the file.</param>
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

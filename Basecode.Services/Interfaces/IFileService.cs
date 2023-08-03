using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basecode.Services.Interfaces
{
    public interface IFileService
    {
        void DeleteFile(string path);
        void SaveFile(string path, byte[] data);
    }
}

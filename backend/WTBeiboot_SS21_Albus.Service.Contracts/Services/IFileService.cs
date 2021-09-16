using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WTBeiboot_SS21_Albus.Service.Contracts.DTO;
using WTBeiboot_SS21_Albus.Service.Contracts.DTO.ExifDTO;

namespace WTBeiboot_SS21_Albus.Service.Contracts.Services
{
    public interface IFileService
    {
        Task<IEnumerable<FileDTO>> GetFiles(string path);
        Task<string> GetImageDataJson(string path);
        Task<ExifDTO> GetExifOfFile(string path);
        Task<bool> ChangeExifOfFile(string path, ExifDTO exifData);
    }
}

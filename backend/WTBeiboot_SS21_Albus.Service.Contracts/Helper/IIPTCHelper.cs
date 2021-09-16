using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WTBeiboot_SS21_Albus.Service.Contracts.DTO;
using WTBeiboot_SS21_Albus.Service.Contracts.DTO.ExifDTO;

namespace WTBeiboot_SS21_Albus.Service.Contracts.Helper
{
    public interface IIPTCHelper
    {
        Task<IEnumerable<ExifDataDTO>> GetIPTCProfile(string path, List<ExifDataDTO> currentValues, IEnumerable<IConfigurationSection> section);
        Task<bool> SetIPTCProfile(string path, IEnumerable<ExifDataDTO> exifData);
    }
}

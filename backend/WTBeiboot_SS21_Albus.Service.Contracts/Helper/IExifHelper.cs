using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WTBeiboot_SS21_Albus.Service.Contracts.DTO;
using WTBeiboot_SS21_Albus.Service.Contracts.DTO.ExifDTO;
using Microsoft.Extensions.Configuration;

namespace WTBeiboot_SS21_Albus.Service.Contracts.Helper
{
    public interface IExifHelper
    {
        Task<IEnumerable<ExifDataDTO>> GetExifProfile(string path, List<ExifDataDTO> currentValues, IEnumerable<IConfigurationSection> section);
    }
}

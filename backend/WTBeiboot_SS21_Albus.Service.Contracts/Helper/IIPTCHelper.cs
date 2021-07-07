using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WTBeiboot_SS21_Albus.Service.Contracts.DTO;

namespace WTBeiboot_SS21_Albus.Service.Contracts.Helper
{
    public interface IIPTCHelper
    {
        Task<IEnumerable<ExifDTO>> GetIPTCProfile(string path, List<ExifDTO> currentValues, IEnumerable<IConfigurationSection> section);
    }
}

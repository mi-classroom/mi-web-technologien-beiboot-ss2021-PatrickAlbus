using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WTBeiboot_SS21_Albus.Service.Contracts.DTO.ConfigurationDTO
{
    public class ConfigurationDTO
    {
        public string Title { get; set; }
        public IEnumerable<ConfigurationValuesDTO> Values { get; set; }
    }
}

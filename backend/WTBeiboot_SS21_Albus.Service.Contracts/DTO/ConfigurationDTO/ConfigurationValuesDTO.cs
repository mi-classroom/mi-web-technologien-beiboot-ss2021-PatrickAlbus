using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WTBeiboot_SS21_Albus.Service.Contracts.DTO.ConfigurationDTO
{
    public class ConfigurationValuesDTO
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string MaxLenght { get; set; }
        public IEnumerable<ConfigurationValueLanguagesDTO> Languages { get; set; } 
    }
}

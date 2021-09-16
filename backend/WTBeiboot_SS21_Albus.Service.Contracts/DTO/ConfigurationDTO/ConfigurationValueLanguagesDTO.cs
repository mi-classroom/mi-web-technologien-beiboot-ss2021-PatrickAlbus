using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WTBeiboot_SS21_Albus.Service.Contracts.DTO.ConfigurationDTO
{
    public class ConfigurationValueLanguagesDTO
    {
        public string Shortcut { get; set; }
        public string Label { get; set; }
        public bool IsMainLanguage { get; set; }
    }
}

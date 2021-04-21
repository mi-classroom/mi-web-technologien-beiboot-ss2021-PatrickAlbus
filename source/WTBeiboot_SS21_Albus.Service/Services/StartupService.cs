using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WTBeiboot_SS21_Albus.Service.Contracts.Services;
using WTBeiboot_SS21_Albus.Service.Contracts.DTO;

namespace WTBeiboot_SS21_Albus.Service.Services
{
    public class StartupService : IStartupService
    {
        private readonly IConfiguration _configuration;
        public StartupService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<IEnumerable<ConfigurationDTO>> GetConfig()
        {
            List<ConfigurationDTO> response = new List<ConfigurationDTO>();

            var valuesSection = _configuration.GetSection("Settings:Configuration");
            foreach(IConfigurationSection section in valuesSection.GetChildren())
            {
                response.Add(new ConfigurationDTO
                {
                    Title = section.GetValue<string>("Title"),
                    Values = section.GetSection("Values")?.GetChildren()?.Select(x => x.Value)?.ToList()
                });
            }

            return response;
        }
    }
}

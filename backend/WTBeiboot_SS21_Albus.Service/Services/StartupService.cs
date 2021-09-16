using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WTBeiboot_SS21_Albus.Service.Contracts.Services;
using WTBeiboot_SS21_Albus.Service.Contracts.DTO.ConfigurationDTO;

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
            foreach (IConfigurationSection section in valuesSection.GetChildren())
            {
                response.Add(new ConfigurationDTO
                {
                    Title = section.GetValue<string>("Title"),
                    Values = GetConfigValues(section.GetSection("Values")?.GetChildren())
                });
            }

            return response;
        }

        private IEnumerable<ConfigurationValuesDTO> GetConfigValues(IEnumerable<IConfigurationSection> configuration)
        {
            var response = new List<ConfigurationValuesDTO>();

            foreach(IConfigurationSection _configuration in configuration)
            {
                response.Add(new ConfigurationValuesDTO
                {
                    Name = _configuration.GetSection("Name")?.Value,
                    Type = _configuration.GetSection("Type")?.Value,
                    MaxLength = _configuration.GetSection("MaxLength")?.Value,
                    IsEditable = (_configuration.GetSection("IsEditable")?.Value.ToLower() == "true") ? true : false,
                    Languages = GetConfigValuesLanguages(_configuration.GetSection("Languages")?.GetChildren())
                });
            }

            return response; 
        }

        private IEnumerable<ConfigurationValueLanguagesDTO> GetConfigValuesLanguages(IEnumerable<IConfigurationSection> configuration)
        {
            var response = new List<ConfigurationValueLanguagesDTO>();

            foreach (IConfigurationSection _configuration in configuration)
            {
                response.Add(new ConfigurationValueLanguagesDTO
                {
                    Shortcut = _configuration.GetSection("Shortcut")?.Value,
                    Label = _configuration.GetSection("Label")?.Value,
                    IsMainLanguage = (_configuration.GetSection("IsMainLanguage")?.Value.ToLower() == "true") ? true : false
                });
            }

            return response;
        }
    }
}
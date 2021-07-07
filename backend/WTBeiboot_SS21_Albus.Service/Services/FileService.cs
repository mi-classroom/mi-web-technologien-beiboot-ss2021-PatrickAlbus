using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using WTBeiboot_SS21_Albus.Service.Contracts.Services;
using WTBeiboot_SS21_Albus.Service.Contracts.Helper;
using WTBeiboot_SS21_Albus.Service.Contracts.DTO;
using MetadataExtractor;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using SixLabors.ImageSharp.Metadata.Profiles.Exif;
using SixLabors.ImageSharp.Metadata.Profiles.Iptc;
using SixLabors.ImageSharp;

namespace WTBeiboot_SS21_Albus.Service.Services
{
    public class FileService : IFileService
    {
        private readonly IConfiguration _configuration;
        private readonly IHostEnvironment _hostEnvironment;
        private readonly IExifHelper _exifHelper;
        private readonly IIPTCHelper _iptcHelper;
        public FileService(IConfiguration configuration,  IHostEnvironment hostEnvironment, IExifHelper exifHelper, IIPTCHelper iptcHelper)
        {
            _configuration = configuration;
            _hostEnvironment = hostEnvironment;
            _exifHelper = exifHelper;
            _iptcHelper = iptcHelper;
    }

        public async Task<IEnumerable<FileDTO>> GetFiles(string path)
        {
            List<FileDTO> response = new List<FileDTO>();

            if (!string.IsNullOrWhiteSpace(path))
            {
                DirectoryInfo dir = new DirectoryInfo(path);

                if (dir.GetFiles().Length > 0)
                {
                    List<string> allowedExtensions = _configuration.GetSection("Settings:FilePattern")?.GetChildren()?.Select(x => x.Value)?.ToList();

                    foreach (FileInfo f in dir.GetFiles().Where(file => allowedExtensions.Any(file.FullName.ToLower().EndsWith)))
                    {
                        response.Add(
                            new FileDTO
                            {
                                FileName = f.FullName.Split(@"\").Last().Split(@"/").Last(),
                                FilePath = f.FullName
                            }
                        );
                    }
                }
            }
            if(response.Count > 0) return response;
            return null;
        }

        public async Task<string> GetImageDataJson(string path)
        {
            try
            {
                string jsonString = File.ReadAllText(path + "/" + _configuration.GetValue<String>("Settings:ImageJson"));
                
                return jsonString;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<IEnumerable<ExifDTO>> GetExifOfFile(string path)
        {
            IEnumerable<ExifDTO> response = new List<ExifDTO>();
            if (File.Exists(path))
            {
                var valuesSection = _configuration.GetSection("Settings:Configuration");
                foreach (IConfigurationSection section in valuesSection.GetChildren())
                {
                    if (section.GetValue<string>("Title") == "Exif" && section.GetSection("Values")?.GetChildren().ToList().Count != 0) response = await _exifHelper.GetExifProfile(path, response.Cast<ExifDTO>().ToList(), section.GetSection("Values")?.GetChildren());
                    if (section.GetValue<string>("Title") == "IPTC" && section.GetSection("Values")?.GetChildren().ToList().Count != 0) response = await _iptcHelper.GetIPTCProfile(path, response.Cast<ExifDTO>().ToList(), section.GetSection("Values")?.GetChildren());
                 }
                return response;
            }

            return null;
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using WTBeiboot_SS21_Albus.Service.Contracts.Services;
using WTBeiboot_SS21_Albus.Service.Contracts.Helper;
using WTBeiboot_SS21_Albus.Service.Contracts.DTO;
using WTBeiboot_SS21_Albus.Service.Contracts.DTO.ExifDTO;
using MetadataExtractor;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using SixLabors.ImageSharp.Metadata.Profiles.Exif;
using SixLabors.ImageSharp.Metadata.Profiles.Iptc;
using SixLabors.ImageSharp;
using System.Drawing;

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

        public async Task<ExifDTO> GetExifOfFile(string path)
        {
            IEnumerable<ExifDataDTO> exifData = new List<ExifDataDTO>();
            if (File.Exists(path))
            {
                var valuesSection = _configuration.GetSection("Settings:Configuration");
                foreach (IConfigurationSection section in valuesSection.GetChildren())
                {
                    if (section.GetValue<string>("Title") == "Exif" && section.GetSection("Values")?.GetChildren().ToList().Count != 0) exifData = await _exifHelper.GetExifProfile(path, exifData.Cast<ExifDataDTO>().ToList(), section.GetSection("Values")?.GetChildren());
                    if (section.GetValue<string>("Title") == "IPTC" && section.GetSection("Values")?.GetChildren().ToList().Count != 0) exifData = await _iptcHelper.GetIPTCProfile(path, exifData.Cast<ExifDataDTO>().ToList(), section.GetSection("Values")?.GetChildren());
                }

                Bitmap bitmap = new Bitmap(path);

                return new ExifDTO
                {
                    Size = bitmap.Width + "x" + bitmap.Height,
                    ExifData = exifData
                }; ;
            }

            return null;
        }

        public async Task<bool> ChangeExifOfFile(string path, ExifDTO exifData)
        {
            var response = await _iptcHelper.SetIPTCProfile(path, exifData.ExifData);
            if (!response) return false;
            return true;
        }

    }
}

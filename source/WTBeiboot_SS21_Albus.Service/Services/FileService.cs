using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using WTBeiboot_SS21_Albus.Service.Contracts.Services;
using WTBeiboot_SS21_Albus.Service.Contracts.DTO;
using MetadataExtractor;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;

namespace WTBeiboot_SS21_Albus.Service.Services
{
    public class FileService : IFileService
    {
        private readonly IConfiguration _configuration;
        private readonly IHostEnvironment _hostEnvironment;

        public FileService(IConfiguration configuration,  IHostEnvironment hostEnvironment)
        {
            _configuration = configuration;
            _hostEnvironment = hostEnvironment;
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

        public async Task<dynamic> GetExifOfFile(string path)
        {
            if(File.Exists(path)) return ImageMetadataReader.ReadMetadata(path);

            return null;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using WTBeiboot_SS21_Albus.Service.Contracts.Services;
using System.Web;
using Microsoft.Extensions.Hosting;
using WTBeiboot_SS21_Albus.Service.Contracts.DTO;
using Microsoft.Extensions.Configuration;
using WTBeiboot_SS21_Albus.Logger;
using System.IO.Compression;

namespace WTBeiboot_SS21_Albus.Service.Services
{
    public class DirectoryService : IDirectoryService
    {
        private readonly IConfiguration _configuration;
        private readonly IHostEnvironment _hostEnvironment;
        private readonly IFileService _fileService;
        private readonly ILoggerManager _loggerManager;

        public DirectoryService(IConfiguration configuration, IHostEnvironment hostEnvironment, IFileService fileService, ILoggerManager loggerManager)
        {
            _configuration = configuration;
            _hostEnvironment = hostEnvironment;
            _fileService = fileService;
            _loggerManager = loggerManager;
        }

        public async Task<DirectoryDTO> GetDirectory(string path = null, string previousPath = null)
        {
#if DEBUG
            if(path == null) path = "D:\\Uni\\Master\\Semester 2\\WebTechnologien\\BeibootProjekt\\mi-web-technologien-beiboot-ss2021-PatrickAlbus\\data";
#endif

            DirectoryDTO response = new DirectoryDTO();

            string currentPath = (path == null) ? _configuration.GetValue<String>("Settings:TargetDirectory") : path;

            _loggerManager.LogInfo(currentPath);

            if (!string.IsNullOrWhiteSpace(currentPath))
            {
                if (!Directory.Exists(currentPath)) return null;

                DirectoryInfo dir = new DirectoryInfo(currentPath);

                DirectoryDTO _directoryDTO = new DirectoryDTO
                {
                    DirectoryName = dir.FullName.Split(@"\").Last().Split(@"/").Last(),
                    DirectoryPath = dir.FullName,
                    ImageDataJson = _fileService.GetImageDataJson(dir.FullName).Result != null ? _fileService.GetImageDataJson(dir.FullName).Result : null, 
                    ChildDirectories = null,
                    Files = _fileService.GetFiles(dir.FullName).Result
                };

                if (dir.GetDirectories().Length > 0)
                {
                    List<DirectoryDTO> _childDirectories = new List<DirectoryDTO>();
                    foreach (DirectoryInfo g in dir.GetDirectories())
                    {
                        _childDirectories.Add(new DirectoryDTO
                        {
                            DirectoryName = g.FullName.Split(@"\").Last().Split(@"/").Last(),
                            DirectoryPath = g.FullName,
                            ImageDataJson = _fileService.GetImageDataJson(g.FullName).Result != null ? _fileService.GetImageDataJson(g.FullName).Result : null,
                            ChildDirectories = null,
                            Files = null
                        });
                    }
                    _directoryDTO.ChildDirectories = _childDirectories;
                }
                response = _directoryDTO;
            }
            return response;
        }

        public (string fileType, byte[] archiveData, string archiveName) DownloadDirectory(string path)
        {
            var zipName = $"{path.Split(@"\").Last().Split(@"/").Last()}-{DateTime.Now.ToString("yyyy_MM_dd-HH_mm_ss")}.zip";

            using (var memoryStream = new MemoryStream())
            {
                using (var archive = new ZipArchive(memoryStream, ZipArchiveMode.Create, true))
                {
                    GetSubDirectorys(path, archive);
                    GetDirectoryFiles(path, archive);
                }

                return ("application/zip", memoryStream.ToArray(), zipName);
            }
        }

        private void GetSubDirectorys(string path, ZipArchive archive)
        {
            DirectoryInfo dir = new DirectoryInfo(path);

            foreach (DirectoryInfo g in dir.GetDirectories())
            {
                GetDirectoryFiles(g.FullName, archive);
            }
        }
        private void GetDirectoryFiles(string path, ZipArchive archive)
        {
            DirectoryInfo dir = new DirectoryInfo(path);

            var files = Directory.GetFiles(Path.Combine(_hostEnvironment.ContentRootPath, path)).ToList();

            List<string> allowedExtensions = _configuration.GetSection("Settings:FilePattern")?.GetChildren()?.Select(x => x.Value)?.ToList();
            allowedExtensions.Add(".json");

            foreach (FileInfo file in dir.GetFiles().Where(file => allowedExtensions.Any(file.FullName.ToLower().EndsWith)))
            {
                var theFile = archive.CreateEntry(file.FullName);
                using (var streamWriter = new StreamWriter(theFile.Open()))
                {
                    streamWriter.Write(File.ReadAllText(file.FullName));
                }
            }
        }
    }
}

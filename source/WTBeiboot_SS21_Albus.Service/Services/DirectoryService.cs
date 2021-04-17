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

namespace WTBeiboot_SS21_Albus.Service.Services
{
    public class DirectoryService : IDirectoryService
    {
        private readonly IConfiguration _configuration;
        private readonly IHostEnvironment _hostEnvironment;
        private readonly IFileService _fileService;
        
        public DirectoryService(IConfiguration configuration, IHostEnvironment hostEnvironment, IFileService fileService)
        {
            _configuration = configuration;
            _hostEnvironment = hostEnvironment;
            _fileService = fileService;
        }

        public async Task<IEnumerable<DirectoryDTO>> GetDirectory(string path = null, string previousPath = null)
        {
            List<DirectoryDTO> response = new List<DirectoryDTO>();


            string currentPath = (path == null) ? _hostEnvironment.ContentRootPath + _configuration.GetValue<String>("Settings:TargetDirectory") : path;
            if (path == null) previousPath = _hostEnvironment.ContentRootPath;
            
            if (!string.IsNullOrWhiteSpace(currentPath))
            {
                if (!Directory.Exists(currentPath)) return null;

                DirectoryInfo dir = new DirectoryInfo(currentPath);

                DirectoryDTO _directoryDTO = new DirectoryDTO
                    {
                        DirectoryName = dir.FullName.Replace(previousPath + "\\", ""),
                        DirectoryPath = dir.FullName.Replace(_hostEnvironment.ContentRootPath, ""),
                        ChildDirectories = null,
                        Files = _fileService.GetFiles(dir.FullName).Result
                };

                if (dir.GetDirectories().Length > 0)
                {
                    List<DirectoryDTO> _childDirectories = new List<DirectoryDTO>();
                    foreach (DirectoryInfo g in dir.GetDirectories())
                    {
                        foreach(DirectoryDTO _tmp in GetDirectory(g.FullName, currentPath).Result)
                        {
                            _childDirectories.Add(_tmp);
                        }
                    }
                    _directoryDTO.ChildDirectories = _childDirectories;
                }
                response.Add(_directoryDTO);
            }
            return response;
        }
    }
}

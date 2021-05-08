﻿using System;
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

        public async Task<IEnumerable<DirectoryDTO>> GetDirectory(string path = null, string previousPath = null)
        {
#if DEBUG
            if(path == null) path = "D:\\Uni\\Master\\Semester 2\\WebTechnologien\\BeibootProjekt\\mi-web-technologien-beiboot-ss2021-PatrickAlbus\\data";
#endif

            List<DirectoryDTO> response = new List<DirectoryDTO>();

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
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using WTBeiboot_SS21_Albus.Service.Contracts.Services;
using WTBeiboot_SS21_Albus.Service.Contracts.DTO;
using Microsoft.Extensions.Hosting;
using System.Linq;
using System.ComponentModel.DataAnnotations;

namespace WTBeiboot_SS21_Albus.Controllers
{
    [Route("api/directories")]
    public class DirectoryController : Controller
    {
        private readonly IDirectoryService _service;
        private readonly IHostEnvironment _hostEnvironment;
        public DirectoryController(IDirectoryService service, IHostEnvironment hostEnvironment)
        {
            _service = service;
            _hostEnvironment = hostEnvironment;
        }

        [ProducesResponseType(typeof(DirectoryDTO), 200)]
        [ProducesResponseType(404)]
        [HttpGet("")]
        public async Task<IActionResult> GetDirectory(string path = null)
        {
            string previousPath = (path != null) ? path.Replace("\\" + path.Split("\\").Last(), "") : null;
            
            var response = _service.GetDirectory(path, previousPath) ;

            if (response == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(response.Result);
            }
        }

        [ProducesResponseType(typeof(FileContentResult), 200)]
        [ProducesResponseType(404)]
        [HttpGet("download")]
        public async Task<IActionResult> DownloadDirectory([Required]string path)
        {
            try
            {
                var (fileType, archiveData, archiveName) = _service.DownloadDirectory(path);

                return File(archiveData, fileType, archiveName);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

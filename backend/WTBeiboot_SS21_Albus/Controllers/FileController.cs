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
using WTBeiboot_SS21_Albus.Service.Contracts.DTO.ExifDTO;
using WTBeiboot_SS21_Albus.Logger;
using System.Web;

namespace WTBeiboot_SS21_Albus.Controllers
{
    [Route("api/files")]
    public class FileController : Controller
    {
        private readonly IFileService _service;
        private readonly ILoggerManager _loggerManager;
        public FileController(IFileService service, ILoggerManager loggerManager)
        {
            _service = service;
            _loggerManager = loggerManager;
        }

        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [HttpGet("{path}")]
        public async Task<IActionResult> GetFile(string path)
        {
            _loggerManager.LogInfo(HttpUtility.UrlDecode(path));
            byte[] b = System.IO.File.ReadAllBytes(HttpUtility.UrlDecode(path));
            return Ok(File(b, "image/jpeg"));
        }

        [ProducesResponseType(typeof(ExifDTO), 200)]
        [ProducesResponseType(404)]
        [HttpGet("exif/{path}")]
        public async Task<IActionResult> GetExifOfFile(string path)
        {
            _loggerManager.LogInfo(HttpUtility.UrlDecode(path));
            var response = _service.GetExifOfFile(HttpUtility.UrlDecode(path));

            if (response == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(response.Result);
            }
        }

        [ProducesResponseType(typeof(bool), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(409)]
        [HttpPut("{path}")]
        public async Task<IActionResult> UpdateExifOfFile(string path, [FromBody] ExifDTO exifData)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model object");
                }

                var response = await _service.ChangeExifOfFile(HttpUtility.UrlDecode(path), exifData);

                if (!response)
                {
                    return BadRequest("Update of metadata failed");
                }

                return Ok(response);
            }
            catch (Exception ex)
            {
                string message = "Unknown server exception";
                if (ex != null)
                {
                    message = ex.Message + (ex.InnerException?.Message != "" ? " " + ex.InnerException?.Message : "");
                    message = message.Replace(" See the inner exception for details.", "");
                }
                return StatusCode(500, new { message });
            }

        }
    }
}

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

namespace WTBeiboot_SS21_Albus.Controllers
{
    [Route("api/files")]
    public class FileController : Controller
    {
        private readonly IFileService _service;
        public FileController(IFileService service)
        {
            _service = service;
        }

        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [HttpGet("{path}")]
        public async Task<IActionResult> GetExifOfFile(string path)
        {
            var response = _service.GetExifOfFile(path);

            if (response == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(response.Result);
            }
        }
    }
}

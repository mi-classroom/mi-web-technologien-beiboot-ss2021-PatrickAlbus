using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WTBeiboot_SS21_Albus.Service.Contracts.Services;
using WTBeiboot_SS21_Albus.Service.Contracts.DTO;

namespace WTBeiboot_SS21_Albus.Controllers
{
    [Route("api/startup")]
    public class StartupController : Controller
    {
        private readonly IStartupService _service;
        public StartupController(IStartupService service)
        {
            _service = service;
        }

        [ProducesResponseType(typeof(IEnumerable<ConfigurationDTO>), 200)]
        [ProducesResponseType(404)]
        [HttpGet("")]
        public async Task<IActionResult> GetConfig()
        {
            var response = _service.GetConfig();

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

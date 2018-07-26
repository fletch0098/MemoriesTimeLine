using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MTL.Library.Models.Common;
using MTL.Library.Common;

namespace MTL.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConfigurationController : ControllerBase
    {
        private readonly AppSettings _appSettings;
        private readonly Constants _constants;

        public ConfigurationController(IOptions<AppSettings> appSettings, IOptions<Constants> constants)
        {
            _appSettings = appSettings.Value;
            _constants = constants.Value;
        }

        [HttpGet("[action]")]
        public ActionResult<AppSettings> AppSettings()
        {

            return _appSettings;
        }

        [HttpGet("[action]")]
        public ActionResult<Constants> Constants()
        {

            return _constants;
        }
    }
}
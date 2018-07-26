using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MTL.Library.Models.Common;
using MTL.Library.Common;

namespace MTL.AngularUI.Controllers
{ 
    [Route("api/[controller]")]
    public class ConfigurationController : Controller
    {
        private readonly AppSettings _appSettings;
        private readonly Constants _constants;

        public ConfigurationController(IOptions<AppSettings> appSettings, IOptions<Constants> constants)
        {
            _appSettings = appSettings.Value;
            _constants = constants.Value;
        }

        [HttpGet("[action]")]
        public ActionResult<AppSettings> GetAppSettings()
        {
            return _appSettings;
        }

        [HttpGet("[action]")]
        public ActionResult<Constants> GetConstants()
        {
            return _constants;
        }
    }
}

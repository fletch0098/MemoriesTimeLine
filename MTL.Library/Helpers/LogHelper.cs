using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Logging;

namespace MTL.Library.Helpers
{
    public class LogHelper
    {
        private readonly ILogger<LogHelper> _logger;

        public LogHelper(ILogger<LogHelper> logger)
        {
            _logger = logger;
        }
        public void LogTest()
        {
            _logger.LogInformation("Test Log");
        }



    }
}

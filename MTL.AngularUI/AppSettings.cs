using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MTL.Library.Models.Common;

namespace MTL.AngularUI
{
    public class AppSettings : MTL.Library.Models.Common.AppSettings
    {
        public string enviromentName { get; set; }
        public string appName { get; set; }
        public string appURL { get; set; }
    }
}

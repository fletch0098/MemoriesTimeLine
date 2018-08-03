using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using MTL.DataAccess;
using MTL.Library.Models.Entities;
using MTL.Library.Helpers;
using Microsoft.EntityFrameworkCore;

namespace MTL.DataAccess.DataAccessLayer
{
    public class AccountManager
    {
        MyAppContext ctx;
        private readonly ILogger<AccountManager> _logger;
        private readonly UserManager<AppUser> _userManager;

        public AccountManager(UserManager<AppUser> userManager, MyAppContext c, ILogger<AccountManager> logger)
        {
            _userManager = userManager;
            ctx = c;
            _logger = logger;
        }

        public string Add()
        {
            string ownerId = "";



            return ownerId;
        }

    }
}

﻿using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using MTL.DataAccess;
using MTL.Library.Models.Entities;
using MTL.Library.Models.Authentication;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MTL.WebAPI.Controllers
{
    [Authorize(Policy = "ApiUser")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DashboardController : Controller
    {
        private readonly ClaimsPrincipal _caller;
        private readonly MyAppContext _appDbContext;

        public DashboardController(UserManager<AppUser> userManager, MyAppContext appDbContext, IHttpContextAccessor httpContextAccessor)
        {
            _caller = httpContextAccessor.HttpContext.User;
            _appDbContext = appDbContext;
        }

        // GET api/dashboard/home
        [HttpGet]
        public async Task<IActionResult> Home()
        {
            // retrieve the user info
            var userId = _caller.Claims.Single(c => c.Type == "id");
            UserProfile person = await _appDbContext.UserProfile.Include(c => c.Identity).SingleAsync(c => c.Identity.Id == userId.Value);

            return new OkObjectResult(new
            {
                Message = "This is secure API and user data!",
                person.Identity.FirstName,
                person.Identity.LastName,
                person.Identity.PictureUrl,
                person.Identity.FacebookId,
                person.Location,
                person.Locale,
                person.Gender
            });
        }

        // GET api/dashboard/getuserdetails
        [HttpGet]
        public async Task<IActionResult> UserDetails()
        {
            // retrieve the user info
            var userId = _caller.Claims.Single(c => c.Type == "id");
            UserProfile person = await _appDbContext.UserProfile.Include(c => c.Identity).SingleAsync(c => c.Identity.Id == userId.Value);

            return new OkObjectResult(new
            {
                person.Identity.FirstName,
                person.Identity.LastName,
                person.Identity.PictureUrl,
                person.Identity.FacebookId,
                person.Location,
                person.Locale,
                person.Gender
            });
        }
    }
}
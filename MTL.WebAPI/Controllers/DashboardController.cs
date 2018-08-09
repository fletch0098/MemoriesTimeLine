using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using MTL.DataAccess;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MTL.DataAccess.Contracts;
using Microsoft.Extensions.Logging;
using MTL.DataAccess.Entities;
using Microsoft.Extensions.Options;
using MTL.Library.Common;
using AutoMapper;
using MTL.Library.Helpers;
using MTL.WebAPI.ViewModels;

namespace MTL.WebAPI.Controllers
{
    [Authorize(Policy = "ApiUser")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DashboardController : Controller
    {
        private readonly ClaimsPrincipal _caller;
        private readonly ILogger<DashboardController> _logger;
        private IRepositoryWrapper _repository;
        private string _entity;
        private readonly Messages _messages;

        public DashboardController(IRepositoryWrapper repository, IOptions<Messages> messages, ILogger<DashboardController> logger, IHttpContextAccessor httpContextAccessor)
        {
            _caller = httpContextAccessor.HttpContext.User;
            _repository = repository;
            _logger = logger;
            _entity = "DashBoard";
            _messages = messages.Value;
        }

        // GET api/dashboard/home
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            // retrieve the user info
            var userId = _caller.Claims.Single(c => c.Type == "id");

            var appUser = await _repository.AppUsers.GetAppUsersByIdentityIdAsync(userId.Value);

            AppUserExtended person = await _repository.AppUsers.GetAppUserWithDetailsAsync(appUser.FirstOrDefault().Id);

            return new OkObjectResult(new
            {
                Message = "This is secure API and user data for ID: " + userId
            });
        }

        // GET api/dashboard/getuserdetails
        [HttpGet]
        public async Task<IActionResult> UserDetails()
        {
            // retrieve the user info
            var userId = _caller.Claims.Single(c => c.Type == "id");

            var appUser = await _repository.AppUsers.GetAppUsersByIdentityIdAsync(userId.Value);

            AppUserExtended person = await _repository.AppUsers.GetAppUserWithDetailsAsync(appUser.FirstOrDefault().Id);

            return new OkObjectResult(new
            {
                person.FirstName,
                person.LastName,
                person.LastModified,
                person.UserProfile.FacebookId,
                person.UserProfile.Location,
                person.UserProfile.PictureUrl,
                person.UserProfile.Locale
            });
        }
    }
}
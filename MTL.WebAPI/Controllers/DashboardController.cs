using System.Linq;
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
        private readonly RepositoryContext _appDbContext;

        public DashboardController(UserManager<IdentityUser> userManager, RepositoryContext appDbContext, IHttpContextAccessor httpContextAccessor)
        {
            _caller = httpContextAccessor.HttpContext.User;
            _appDbContext = appDbContext;
        }

        // GET api/dashboard/home
        [HttpGet]
        public IActionResult Get()
        {
            // retrieve the user info
            var userId = _caller.Claims.Single(c => c.Type == "id");
            //UserProfile person = await _appDbContext.UserProfile.Include(c => c.identity).SingleAsync(c => c.identity.Id == userId.Value);

            return new OkObjectResult(new
            {
                Message = "This is secure API and user data for ID: " + userId
            });
        }

        //// GET api/dashboard/getuserdetails
        //[HttpGet]
        //public async Task<IActionResult> UserDetails()
        //{
        //    // retrieve the user info
        //    var userId = _caller.Claims.Single(c => c.Type == "id");
        //    UserProfile person = await _appDbContext.UserProfile.Include(c => c.identity).SingleAsync(c => c.identity.Id == userId.Value);

        //    return new OkObjectResult(new
        //    {
        //        person.identity.FirstName,
        //        person.identity.LastName,
        //        person.identity.PictureUrl,
        //        person.identity.FacebookId,
        //        person.location,
        //        person.locale,
        //        person.gender
        //    });
        //}
    }
}
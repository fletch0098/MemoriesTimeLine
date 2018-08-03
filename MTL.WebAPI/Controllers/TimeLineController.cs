using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MTL.DataAccess;
using MTL.DataAccess.RepositoryManager;
using MTL.Library.Models.Entities;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace MTL.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TimeLineController : Controller
    {
        private readonly ILogger<TimeLineController> _logger;
        private IDataRepository<TimeLine, int> _iRepo;
        private readonly ClaimsPrincipal _caller;
        private readonly MyAppContext _appDbContext;

        public TimeLineController(IDataRepository<TimeLine, int> repo, ILogger<TimeLineController> logger, IHttpContextAccessor httpContextAccessor, MyAppContext appDbContext)
        {
            _iRepo = repo;
            _logger = logger;
            _caller = httpContextAccessor.HttpContext.User;
            _appDbContext = appDbContext;
        }

        // GET api/[Controller]
        [HttpGet]
        public IActionResult GetAll()
        {
            _logger.LogTrace("GetAll() api/TimeLine");
            IActionResult ret = null;

            var item = _iRepo.GetAll();

            if (item == null)
            {
                ret = NotFound();
            }
            else
            {
                ret = new ObjectResult(item);
            }

            _logger.LogTrace("GetAll() api/TimeLine returned : {0} TimeLines", item.Count());
            return ret;
        }

        // GET api/[Controller]/5
        [HttpGet("{id}", Name = "GetTimeLine")]
        public IActionResult Get(int id)
        {
            _logger.LogDebug("GET api/TimeLine/{0}", id);
            IActionResult ret = null;

            var item = _iRepo.Get(id);

            if (item == null)
            {
                ret = NotFound();
            }
            else
            {
                ret = new ObjectResult(item);
            }
            _logger.LogDebug("GET api/TimeLine/{0} returned : {1}", id, ret);
            return ret;
        }

        [HttpPost]
        public IActionResult Post([FromBody] TimeLine item)
        {
            _logger.LogDebug("GET api/TimeLine/post");
            IActionResult ret = null;

            if (item == null)
            {
                ret = BadRequest();
            }
            else
            {
                var id = _iRepo.Add(item);
                ret = CreatedAtRoute("GetTimeLine", new { id = item.id }, item);
            }

            _logger.LogDebug("GET api/TimeLine/post returned : {0}", ret);
            return ret;
        }

        [HttpPut]
        public IActionResult Put([FromBody] TimeLine item)
        {
            _logger.LogDebug("GET api/TimeLine/Put");
            IActionResult ret = null;

            if (item == null)
            {
                ret = BadRequest();
            }

            else
            {
                var updatedId = _iRepo.Update(item.id, item);

                if (updatedId == 0)
                {
                    ret = NotFound();
                }
                else
                {
                    ret = new ObjectResult(updatedId);
                }
            }
            _logger.LogDebug("GET api/TimeLine/put returned : {0}", ret);
            return ret;
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _logger.LogDebug("GET api/TimeLine/Delete");
            IActionResult ret = null;

            var item = _iRepo.Get(id);

            if (item == null)
            {
                ret = NotFound();
            }
            else
            {
                var deletedId = _iRepo.Delete(id);
                ret = new ObjectResult(deletedId);
            }
            _logger.LogDebug("GET api/TimeLine/Delete returned : {0}", ret);
            return ret;
        }


        //// GET api/[Controller]
        //[HttpGet]
        //public IActionResult GetMyTimeLines()
        //{
        //    _logger.LogTrace("GetAll() api/TimeLine/GetMyTimeLines");
        //    IActionResult ret = null;

        //    // retrieve the user info
        //    var userId = _caller.Claims.Single(c => c.Type == "id");

        //    var item = (from q in _appDbContext.TimeLines.Include("memories")
        //                            where q.ownerId == userId.Value
        //                            select q).ToList();

        //    if (item == null)
        //    {
        //        ret = NotFound();
        //    }
        //    else
        //    {
        //        ret = new ObjectResult(item);
        //    }

        //    _logger.LogTrace("GetAll() api/TimeLine returned : {0} TimeLines", item.Count());
        //    return ret;
        //}
    }
}

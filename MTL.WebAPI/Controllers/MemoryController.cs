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

namespace MTL.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MemoryController : Controller
    {
        private readonly ILogger<MemoryController> _logger;
        private IDataRepository<Memory, int> _iRepo;

        public MemoryController(IDataRepository<Memory, int> repo, ILogger<MemoryController> logger)
        {
            _iRepo = repo;
            _logger = logger;
        }

        // GET api/[Controller]
        [HttpGet]
        public IActionResult GetAll()
        {
            _logger.LogDebug("GET api/Memory");
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

            _logger.LogDebug("GET api/Memory returned : {0}", ret);
            return ret;
        }

        // GET api/[Controller]/5
        [HttpGet("{id}", Name = "GetMemory")]
        public IActionResult Get(int id)
        {
            _logger.LogDebug("GET api/Memory/{0}", id);
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
            _logger.LogDebug("GET api/Memory/{0} returned : {1}", id, ret);
            return ret;
        }

        [HttpPost]
        public IActionResult Post([FromBody] Memory item)
        {
            _logger.LogDebug("GET api/Memory/post");
            IActionResult ret = null;

            if (item == null)
            {
                ret = BadRequest();
            }
            else
            {
                var id = _iRepo.Add(item);
                ret = CreatedAtRoute("GetMemory", new { id = item.id }, item);
            }

            _logger.LogDebug("GET api/Memory/post returned : {0}", ret);
            return ret;
        }

        [HttpPut]
        public IActionResult Put([FromBody] Memory item)
        {
            _logger.LogDebug("GET api/Memory/Put");
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
            _logger.LogDebug("GET api/Memory/put returned : {0}", ret);
            return ret;
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _logger.LogDebug("GET api/Memory/Delete");
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
            _logger.LogDebug("GET api/Memory/Delete returned : {0}", ret);
            return ret;
        }
    }
}

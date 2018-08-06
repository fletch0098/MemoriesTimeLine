using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MTL.DataAccess.Contracts;
using Microsoft.Extensions.Logging;
using MTL.DataAccess.Entities;

namespace MTL.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TimeLineTestController : ControllerBase
    {
        private readonly ILogger<TimeLineTestController> _logger;
        private IRepositoryWrapper _repository;

        public TimeLineTestController(IRepositoryWrapper repository, ILogger<TimeLineTestController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var result = await _repository.TimeLine.GetAllTimeLinesAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Some error in the GetAllTimeLines method: {ex}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}", Name = "GetById")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var result = await _repository.TimeLine.GetTimeLineByIdAsync(id);

                if (result.IsEmptyObject())
                {
                    _logger.LogError($"TimeLine with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                else
                {
                    _logger.LogInformation($"Returned timeLine with id: {id}");
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetTimeLineById action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}/memories")]
        public async Task<IActionResult> GetWithDetails(int id)
        {
            try
            {
                var result = await _repository.TimeLine.GetTimeLineWithDetailsAsync(id);

                if (result.IsEmptyObject())
                {
                    _logger.LogError($"TimeLine with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                else
                {
                    _logger.LogInformation($"Returned timeLine with details for id: {id}");
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetTimeLineWithDetails action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
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
                var id = _repository.TimeLine.CreateTimeLine(item);
                ret = CreatedAtRoute("GetTimeLine", new { id = item.Id }, item);
            }

            _logger.LogDebug("GET api/TimeLine/post returned : {0}", ret);
            return ret;
        }

        //[HttpPost]
        //public async Task<IActionResult> Post([FromBody]TimeLine timeLine)
        //{
        //    try
        //    {
        //        if (timeLine.IsObjectNull())
        //        {
        //            _logger.LogError("TimeLine object sent from client is null.");
        //            return BadRequest("TimeLine object is null");
        //        }

        //        if (!ModelState.IsValid)
        //        {
        //            _logger.LogError("Invalid timeLine object sent from client.");
        //            return BadRequest("Invalid model object");
        //        }

        //        await _repository.TimeLine.CreateTimeLineAsync(timeLine);

        //        return CreatedAtRoute("GetById", new { id = timeLine.Id }, timeLine);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError($"Something went wrong inside CreateTimeLine action: {ex.Message}");
        //        return StatusCode(500, "Internal server error");
        //    }
        //}

        //[HttpPut]
        //public async Task<IActionResult> UpdateTimeLine([FromBody]TimeLine timeLine)
        //{
        //    try
        //    {
        //        if (timeLine.IsObjectNull())
        //        {
        //            _logger.LogError("TimeLine object sent from client is null.");
        //            return BadRequest("TimeLine object is null");
        //        }

        //        if (!ModelState.IsValid)
        //        {
        //            _logger.LogError("Invalid timeLine object sent from client.");
        //            return BadRequest("Invalid model object");
        //        }

        //        var dbTimeLine = await _repository.TimeLine.GetTimeLineByIdAsync(timeLine.Id);
        //        if (dbTimeLine.IsEmptyObject())
        //        {
        //            _logger.LogError($"TimeLine with id: {timeLine.Id}, hasn't been found in db.");
        //            return NotFound();
        //        }

        //        await _repository.TimeLine.UpdateTimeLineAsync(timeLine.Id, timeLine);

        //        return NoContent();
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError($"Something went wrong inside UpdateTimeLine action: {ex.Message}");
        //        return StatusCode(500, "Internal server error");
        //    }
        //}

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTimeLine(int id)
        {
            try
            {
                var timeLine = await _repository.TimeLine.GetTimeLineByIdAsync(id);
                if (timeLine.IsEmptyObject())
                {
                    _logger.LogError($"TimeLine with id: {id}, hasn't been found in db.");
                    return NotFound();
                }

                await _repository.TimeLine.DeleteTimeLineAsync(id);

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside DeleteTimeLine action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
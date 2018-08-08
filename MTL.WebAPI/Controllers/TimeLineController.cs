using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MTL.DataAccess.Contracts;
using Microsoft.Extensions.Logging;
using MTL.DataAccess.Entities;
using Microsoft.Extensions.Options;
using MTL.Library.Common;

namespace MTL.WebAPI.Controllers
{
    /// <summary>
    /// TimeLine Controller Manages all things TimeLine
    /// </summary>
    [Route("api/TimeLine")]
    [ApiController]
    public class TimeLineController : ControllerBase
    {
        private readonly ILogger<TimeLineController> _logger;
        private IRepositoryWrapper _repository;
        private string _entity;
        private readonly Messages _messages;

        /// <summary>
        /// TimeLine Controller Manages all things TimeLine
        /// </summary>
        public TimeLineController(IRepositoryWrapper repository, IOptions<Messages> messages, ILogger<TimeLineController> logger)
        {
            _repository = repository;
            _logger = logger;
            _entity = "TimeLine";
            _messages = messages.Value;

        }

        /// <summary>
        /// Get all TimeLines
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /TimeLine
        ///     {
        ///     
        ///     }
        ///
        /// </remarks>
        /// <returns>A list of Timelines</returns>
        /// <response code="200">TimeLines[]</response>
        /// <response code="500">Internal server error</response>            
        [HttpGet]
        [ProducesResponseType(typeof(TimeLine[]), 200)]
        [ProducesResponseType(typeof(StatusCodeResult), 500)]
        public async Task<IActionResult> GetAllTimeLines()
        {
            try
            {
                var result = await _repository.TimeLines.GetAllTimeLinesAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format(_messages.Controller["SomeError"],ex.Message));
                return StatusCode(500, string.Format(_messages.Controller["Error500"]));
            }
        }

        /// <summary>
        /// Get all TimeLines by AppUserId
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /TimeLine/AppUSer/1
        ///     {
        ///        
        ///     }
        ///
        /// </remarks>
        /// <param name="appUserId">AppUser Id</param>
        /// <returns>A list of TimeLines</returns>
        /// <response code="200">TimeLines[]</response>
        /// <response code="500">Internal server error</response>            
        [HttpGet("appuser/{appUserId}")]
        [ProducesResponseType(typeof(TimeLine[]), 200)]
        [ProducesResponseType(typeof(StatusCodeResult), 500)]
        public async Task<IActionResult> GetAllByAppUSerId(int appUserId)
        {
            try
            {
                var result = await _repository.TimeLines.GetTimeLinesByAppUserIdAsync(appUserId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format(_messages.Controller["SomeError"], ex.Message));
                return StatusCode(500, string.Format(_messages.Controller["Error500"]));
            }
        }


        /// <summary>
        /// Get a TimeLine by Id
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     Get /TimeLine/1
        ///     {
        ///       
        ///     }
        ///
        /// </remarks>
        /// <param name="id">TimeLine Id</param>
        /// <returns>A single TimeLine</returns>
        /// <response code="200">TimeLine</response>
        /// <response code="404">Object with Id not found</response>
        /// <response code="500">Internal server error</response>       
        [HttpGet("{id}", Name = "GetTimeLineById")]
        [ProducesResponseType(typeof(TimeLine), 200)]
        [ProducesResponseType(typeof(NotFoundResult), 404)]
        [ProducesResponseType(typeof(StatusCodeResult), 500)]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var result = await _repository.TimeLines.GetTimeLineByIdAsync(id);

                if (result.IsEmptyObject())
                {
                    _logger.LogError(string.Format(_messages.Controller["NotFound"], _entity,id));
                    return NotFound();
                }
                else
                {
                    _logger.LogError(string.Format(_messages.Controller["ReturnedId"], _entity, id));
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format(_messages.Controller["SomeError"], ex.Message));
                return StatusCode(500, string.Format(_messages.Controller["Error500"]));
            }
        }

        /// <summary>
        /// Get a TimeLine with all it´s details
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     Get /TimeLine/1/Details
        ///     {
        ///        
        ///     }
        ///
        /// </remarks>
        /// <param name="id">TimeLine Id</param>
        /// <returns>A single extended TimeLine</returns>
        /// <response code="200">ExtendedTimeLine</response>
        /// <response code="404">Object with Id not found</response>
        /// <response code="500">Internal server error</response>  
        [HttpGet("{id}/details")]
        [ProducesResponseType(typeof(TimeLineExtended), 204)]
        [ProducesResponseType(typeof(NotFoundResult), 404)]
        [ProducesResponseType(typeof(StatusCodeResult), 500)]
        public async Task<IActionResult> GetWithDetails(int id)
        {
            try
            {
                var result = await _repository.TimeLines.GetTimeLineWithDetailsAsync(id);

                if (result.IsEmptyObject())
                {
                    _logger.LogError(string.Format(_messages.Controller["NotFound"], _entity, id));
                    return NotFound();
                }
                else
                {
                    _logger.LogError(string.Format(_messages.Controller["ReturnedDetails"], _entity, id));
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format(_messages.Controller["SomeError"], ex.Message));
                return StatusCode(500, string.Format(_messages.Controller["Error500"]));
            }
        }

        /// <summary>
        /// Update a TimeLine
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /TimeLine
        ///     {
        ///         "name": "string",
        ///         "description": "string",
        ///         "appUserId": 0,
        ///         "id": 0,
        ///     }
        /// 
        ///
        /// </remarks>
        /// <param name="timeLine">TimeLine</param>
        /// <returns></returns>
        /// <response code="204">No content</response>
        /// <response code="400">Invalid Object</response>
        /// <response code="404">Object with Id not found</response>
        /// <response code="500">Internal server error</response> 
        [HttpPut]
        [ProducesResponseType(typeof(NoContentResult), 204)]
        [ProducesResponseType(typeof(BadRequestObjectResult), 400)]
        [ProducesResponseType(typeof(NotFoundResult), 404)]
        [ProducesResponseType(typeof(StatusCodeResult), 500)]
        public async Task<IActionResult> UpdateTimeLine([FromBody]TimeLine timeLine)
        {
            try
            {
                if (timeLine.IsObjectNull())
                {
                    _logger.LogError(string.Format(_messages.Controller["NullObject"], _entity));
                    return BadRequest(string.Format(_messages.Controller["NullObject"], _entity));
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError(string.Format(_messages.Controller["InvalidObject"], _entity));
                    return BadRequest(string.Format(_messages.Controller["InvalidObject"], _entity));
                }

                var dbTimeLine = await _repository.TimeLines.GetTimeLineByIdAsync(timeLine.Id);
                if (dbTimeLine.IsEmptyObject())
                {
                    _logger.LogError(string.Format(_messages.Controller["NotFound"], _entity, timeLine.Id));
                    return NotFound();
                }

                await _repository.TimeLines.UpdateTimeLineAsync(timeLine.Id, timeLine);
                _logger.LogError(string.Format(_messages.Controller["NotFound"], _entity, timeLine.Id));
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format(_messages.Controller["SomeError"], ex.Message));
                return StatusCode(500, string.Format(_messages.Controller["Error500"]));
            }
        }

        /// <summary>
        /// Create a TimeLine
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /TimeLine/1
        ///     {
        ///         "name": "string",
        ///         "description": "string",
        ///         "appUserId": 0,
        ///     }
        ///
        /// </remarks>
        /// <param name="timeLine">TimeLine</param>
        /// <returns></returns>
        /// <response code="201">Object created at route</response>
        /// <response code="400">Object Null or Invalid</response>
        /// <response code="500">Internal server error</response> 
        [HttpPost]
        [ProducesResponseType(typeof(TimeLine),201)]
        [ProducesResponseType(typeof(BadRequestObjectResult),400)]
        [ProducesResponseType(typeof(StatusCodeResult),500)]
        public async Task<IActionResult> Post([FromBody]TimeLine timeLine)
        {
            try
            {
                if (timeLine.IsObjectNull())
                {
                    _logger.LogError(string.Format(_messages.Controller["NullObject"], _entity));
                    return BadRequest(string.Format(_messages.Controller["NullObject"], _entity));
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError(string.Format(_messages.Controller["InvalidObject"], _entity));
                    return BadRequest(string.Format(_messages.Controller["InvalidObject"], _entity));
                }

                var Id = await _repository.TimeLines.CreateTimeLineAsync(timeLine);

                var ret = CreatedAtRoute("GetTimeLineById", new { id = Id }, timeLine);
                _logger.LogError(string.Format(_messages.Controller["InvalidObject"], _entity));
                return ret;
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format(_messages.Controller["SomeError"], ex.Message));
                return StatusCode(500, string.Format(_messages.Controller["Error500"]));
            }
        }

        /// <summary>
        /// Delete a TimeLine by Id
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     DELETE /TimeLine/1
        ///     {
        ///      
        ///     }
        ///
        /// </remarks>
        /// <param name="id">TimeLine Id</param>
        /// <returns>TimeLine Id</returns>
        /// <response code="200">Object Result Id</response>
        /// <response code="404">Object with Id not found</response>
        /// <response code="500">Internal server error</response>     
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ObjectResult),200)]
        [ProducesResponseType(typeof(NotFoundResult), 404)]
        [ProducesResponseType(typeof(StatusCodeResult), 500)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var timeLine = await _repository.TimeLines.GetTimeLineByIdAsync(id);
                if (timeLine.IsEmptyObject())
                {
                    _logger.LogError(string.Format(_messages.Controller["NotFound"], _entity, id));
                    return NotFound();
                }

                await _repository.TimeLines.DeleteTimeLineAsync(id);
                _logger.LogError(string.Format(_messages.Controller["Deleted"], _entity, id));
                return new ObjectResult(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format(_messages.Controller["SomeError"], ex.Message));
                return StatusCode(500, string.Format(_messages.Controller["Error500"]));
            }
        }
    }
}
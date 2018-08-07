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
                var result = await _repository.TimeLine.GetAllTimeLinesAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format(_messages.Controller["SomeError"],ex.Message));
                return StatusCode(500, string.Format(_messages.Controller["Error500"]));
            }
        }

        /// <summary>
        /// Get all TimeLines by OwnerId
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /TimeLine/owner/1
        ///     {
        ///        
        ///     }
        ///
        /// </remarks>
        /// <param name="ownerId">Owner Id</param>
        /// <returns>A list of TimeLines for the owner</returns>
        /// <response code="200">TimeLines[]</response>
        /// <response code="500">Internal server error</response>            
        [HttpGet("owner/{ownerId}")]
        [ProducesResponseType(typeof(TimeLine[]), 200)]
        [ProducesResponseType(typeof(StatusCodeResult), 500)]
        public async Task<IActionResult> GetAllByOwnerId(string ownerId)
        {
            try
            {
                var result = await _repository.TimeLine.GetTimeLinesByOwnerIdAsync(ownerId);
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
                var result = await _repository.TimeLine.GetTimeLineByIdAsync(id);

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
        /// Get a TimeLine with its Memories
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     Get /TimeLine/1/Memories
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
        [HttpGet("{id}/memories")]
        [ProducesResponseType(typeof(TimeLineExtended), 204)]
        [ProducesResponseType(typeof(NotFoundResult), 404)]
        [ProducesResponseType(typeof(StatusCodeResult), 500)]
        public async Task<IActionResult> GetWithDetails(int id)
        {
            try
            {
                var result = await _repository.TimeLine.GetTimeLineWithDetailsAsync(id);

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
        /// Get a TimeLine with its Owner
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     Get /TimeLine/1/Owner
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
        [HttpGet("{id}/owner")]
        [ProducesResponseType(typeof(TimeLineExtended), 204)]
        [ProducesResponseType(typeof(NotFoundResult), 404)]
        [ProducesResponseType(typeof(StatusCodeResult), 500)]
        public async Task<IActionResult> GetWithOwner(int id)
        {
            try
            {
                var result = await _repository.TimeLine.GetTimeLineWithOwnerAsync(id);

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
        ///         "ownerId": "string",
        ///         "id": 0,
        ///         "lastModified": "2018-08-07T01:39:19.706Z"
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

                var dbTimeLine = await _repository.TimeLine.GetTimeLineByIdAsync(timeLine.Id);
                if (dbTimeLine.IsEmptyObject())
                {
                    _logger.LogError(string.Format(_messages.Controller["NotFound"], _entity, timeLine.Id));
                    return NotFound();
                }

                await _repository.TimeLine.UpdateTimeLineAsync(timeLine.Id, timeLine);
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
        ///         "ownerId": "string",
        ///         "id": 0,
        ///         "lastModified": "2018-08-07T01:39:19.706Z"
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

                var Id = await _repository.TimeLine.CreateTimeLineAsync(timeLine);

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
                var timeLine = await _repository.TimeLine.GetTimeLineByIdAsync(id);
                if (timeLine.IsEmptyObject())
                {
                    _logger.LogError(string.Format(_messages.Controller["NotFound"], _entity, id));
                    return NotFound();
                }

                await _repository.TimeLine.DeleteTimeLineAsync(id);
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
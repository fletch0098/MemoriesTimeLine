using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MTL.DataAccess.Contracts;
using Microsoft.Extensions.Logging;
using MTL.DataAccess.Entities;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MTL.Library.Common;

namespace MTL.WebAPI.Controllers
{
    /// <summary>
    /// TimeLine Controller Manages all things TimeLine
    /// </summary>
    [Route("api/TimeLineTest")]
    [ApiController]
    public class TimeLineTestController : ControllerBase
    {
        private readonly ILogger<TimeLineTestController> _logger;
        private IRepositoryWrapper _repository;
        private string _entity;
        private readonly Messages _messages;

        /// <summary>
        /// TimeLine Controller Manages all things TimeLine
        /// </summary>
        public TimeLineTestController(IRepositoryWrapper repository, IOptions<Messages> messages, ILogger<TimeLineTestController> logger)
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
        ///     }
        ///
        /// </remarks>
        /// <returns>A list of all TimeLines</returns>
        /// <response code="200">Returns a list of all TimeLines</response>
        /// <response code="500">If there is an error</response>            
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetAll()
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
        /// Get all TimeLines
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
        /// <returns>A list of all TimeLines for the owner</returns>
        /// <response code="200">Returns a list of TimeLines</response>
        /// <response code="500">If there is an error</response>            
        [HttpGet("owner/{ownerId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetAllByOwner(string ownerId)
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
        ///        "id": 1
        ///     }
        ///
        /// </remarks>
        /// /// <param name="id">TimeLine Id</param>
        /// <returns>A TimeLine</returns>
        /// <response code="200">Returns the TimeLine</response>
        /// <response code="404">If the TimeLine is not found</response>
        /// <response code="500">If there is an error</response>       
        [HttpGet("{id}", Name = "GetTimeLineById")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(200)]
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
        /// Get a TimeLine with its list of Memories
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     Get /TimeLine/1/Memories
        ///     {
        ///        "id": 1
        ///     }
        ///
        /// </remarks>
        /// <param name="id">TimeLine Id</param>
        /// <returns>An extended TimeLine</returns>
        /// <response code="200">Returns the extended TimeLine</response>
        /// <response code="404">If the TimeLine is not found</response>
        /// <response code="500">If there is an error</response>  
        [HttpGet("{id}/memories")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
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
        /// Get a TimeLine with its owner
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     Get /TimeLine/1/Owner
        ///     {
        ///        "id": 1
        ///     }
        ///
        /// </remarks>
        /// <param name="id">TimeLine Id</param>
        /// <returns>An extended TimeLine</returns>
        /// <response code="200">Returns the extended TimeLine</response>
        /// <response code="404">If the TimeLine is not found</response>
        /// <response code="500">If there is an error</response>  
        [HttpGet("{id}/owner")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
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
        ///     PUT /TimeLine/1
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
        /// <response code="400">If the object is not a valid TimeLine</response>
        /// <response code="404">If the TimeLine is not found</response>
        /// <response code="500">If there is an error</response> 
        [HttpPut]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
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
        /// <param name="timeLine">A TimeLine object</param>
        /// <returns></returns>
        /// <response code="201">Created at route</response>
        /// <response code="400">If the object is not a valid TimeLine</response>
        /// <response code="500">If there is an error</response> 
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
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

                var testId = await _repository.TimeLine.CreateTimeLineAsync(timeLine);

                var ret = CreatedAtRoute("GetTimeLineTest", new { id = testId }, timeLine);
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
        ///        "id": 1
        ///     }
        ///
        /// </remarks>
        /// /// <param name="id">TimeLine Id</param>
        /// <returns>A TimeLine</returns>
        /// <response code="200">Returns the TimeLine</response>
        /// <response code="404">If the TimeLine is not found</response>
        /// <response code="500">If there is an error</response>     
        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(200)]
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
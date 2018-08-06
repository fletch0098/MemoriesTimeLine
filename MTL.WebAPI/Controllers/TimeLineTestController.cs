using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MTL.DataAccess.Contracts;
using Microsoft.Extensions.Logging;
using MTL.DataAccess.Entities;

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

        /// <summary>
        /// TimeLine Controller Manages all things TimeLine
        /// </summary>
        public TimeLineTestController(IRepositoryWrapper repository, ILogger<TimeLineTestController> logger)
        {
            _repository = repository;
            _logger = logger;
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
                _logger.LogError($"Some error in the GetAllTimeLines method: {ex}");
                return StatusCode(500, "Internal server error");
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
        /// /// <param name="id">Id of the TimeLine</param>
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
        /// <param name="id">Id of the TimeLine</param>
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

        /// <summary>
        /// Update a TimeLine
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /TimeLine/1
        ///     {
        ///        "id": 1,
        ///        ........
        ///     }
        ///
        /// </remarks>
        /// <param name="timeLine">A TimeLine object</param>
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
                    _logger.LogError("TimeLine object sent from client is null.");
                    return BadRequest("TimeLine object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid timeLine object sent from client.");
                    return BadRequest("Invalid model object");
                }

                var dbTimeLine = await _repository.TimeLine.GetTimeLineByIdAsync(timeLine.Id);
                if (dbTimeLine.IsEmptyObject())
                {
                    _logger.LogError($"TimeLine with id: {timeLine.Id}, hasn't been found in db.");
                    return NotFound();
                }

                await _repository.TimeLine.UpdateTimeLineAsync(timeLine.Id, timeLine);

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside UpdateTimeLine action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>
        /// Create a TimeLine
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /TimeLine
        ///     {
        ///        "id": 1,
        ///        ........
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
                    _logger.LogError("TimeLine object sent from client is null.");
                    return BadRequest("TimeLine object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid timeLine object sent from client.");
                    return BadRequest("Invalid model object");
                }

                var testId = await _repository.TimeLine.CreateTimeLineAsync(timeLine);

                var ret = CreatedAtRoute("GetTimeLineTest", new { id = testId }, timeLine);

                return ret;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside CreateTimeLine action: {ex.Message}");
                return StatusCode(500, "Internal server error");
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
        /// /// <param name="id">Id of the TimeLine</param>
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
                    _logger.LogError($"TimeLine with id: {id}, hasn't been found in db.");
                    return NotFound();
                }

                await _repository.TimeLine.DeleteTimeLineAsync(id);

                return new ObjectResult(id);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside DeleteTimeLine action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
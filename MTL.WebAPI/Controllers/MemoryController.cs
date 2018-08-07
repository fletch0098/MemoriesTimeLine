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
    /// Memory Controller Manages all things Memory
    /// </summary>
    [Route("api/Memory")]
    [ApiController]
    public class MemoryController : ControllerBase
    {
        private readonly ILogger<MemoryController> _logger;
        private IRepositoryWrapper _repository;
        private string _entity;
        private readonly Messages _messages;

        /// <summary>
        /// Memory Controller Manages all things Memory
        /// </summary>
        public MemoryController(IRepositoryWrapper repository, IOptions<Messages> messages, ILogger<MemoryController> logger)
        {
            _repository = repository;
            _logger = logger;
            _entity = "Memory";
            _messages = messages.Value;

        }

        /// <summary>
        /// Get all Memories
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /Memory
        ///     {
        ///     
        ///     }
        ///
        /// </remarks>
        /// <returns>A list of Memories</returns>
        /// <response code="200">Memory[]</response>
        /// <response code="500">Internal server error</response>            
        [HttpGet]
        [ProducesResponseType(typeof(Memory[]), 200)]
        [ProducesResponseType(typeof(StatusCodeResult), 500)]
        public async Task<IActionResult> GetAllMemories()
        {
            try
            {
                var result = await _repository.Memory.GetAllMemoriesAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format(_messages.Controller["SomeError"], ex.Message));
                return StatusCode(500, string.Format(_messages.Controller["Error500"]));
            }
        }

        ///// <summary>
        ///// Get all Memories by OwnerId
        ///// </summary>
        ///// <remarks>
        ///// Sample request:
        /////
        /////     GET /Memory/owner/1
        /////     {
        /////        
        /////     }
        /////
        ///// </remarks>
        ///// <param name="ownerId">Owner Id</param>
        ///// <returns>A list of Memories for the owner</returns>
        ///// <response code="200">Memories[]</response>
        ///// <response code="500">Internal server error</response>            
        //[HttpGet("owner/{ownerId}")]
        //[ProducesResponseType(typeof(Memory[]), 200)]
        //[ProducesResponseType(typeof(StatusCodeResult), 500)]
        //public async Task<IActionResult> GetAllByOwnerId(string ownerId)
        //{
        //    try
        //    {
        //        var result = await _repository.Memory.GetMemoriesByOwnerIdAsync(ownerId);
        //        return Ok(result);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(string.Format(_messages.Controller["SomeError"], ex.Message));
        //        return StatusCode(500, string.Format(_messages.Controller["Error500"]));
        //    }
        //}


        /// <summary>
        /// Get a Memory by Id
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     Get /Memory/1
        ///     {
        ///       
        ///     }
        ///
        /// </remarks>
        /// <param name="id">Memory Id</param>
        /// <returns>A single Memory</returns>
        /// <response code="200">Memory</response>
        /// <response code="404">Object with Id not found</response>
        /// <response code="500">Internal server error</response>       
        [HttpGet("{id}", Name = "GetMemoryById")]
        [ProducesResponseType(typeof(Memory), 200)]
        [ProducesResponseType(typeof(NotFoundResult), 404)]
        [ProducesResponseType(typeof(StatusCodeResult), 500)]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var result = await _repository.Memory.GetMemoryByIdAsync(id);

                if (result.IsEmptyObject())
                {
                    _logger.LogError(string.Format(_messages.Controller["NotFound"], _entity, id));
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
        /// Get a Memory with its TimeLine
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     Get /Memory/1/TimeLine
        ///     {
        ///        
        ///     }
        ///
        /// </remarks>
        /// <param name="id">Memory Id</param>
        /// <returns>A single extended Memory</returns>
        /// <response code="200">ExtendedMemory</response>
        /// <response code="404">Object with Id not found</response>
        /// <response code="500">Internal server error</response>  
        [HttpGet("{id}/memories")]
        [ProducesResponseType(typeof(MemoryExtended), 204)]
        [ProducesResponseType(typeof(NotFoundResult), 404)]
        [ProducesResponseType(typeof(StatusCodeResult), 500)]
        public async Task<IActionResult> GetWithDetails(int id)
        {
            try
            {
                var result = await _repository.Memory.GetMemoryWithDetailsAsync(id);

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

        ///// <summary>
        ///// Get a Memory with its Owner
        ///// </summary>
        ///// <remarks>
        ///// Sample request:
        /////
        /////     Get /Memory/1/Owner
        /////     {
        /////       
        /////     }
        /////
        ///// </remarks>
        ///// <param name="id">Memory Id</param>
        ///// <returns>A single extended Memory</returns>
        ///// <response code="200">ExtendedMemory</response>
        ///// <response code="404">Object with Id not found</response>
        ///// <response code="500">Internal server error</response>  
        //[HttpGet("{id}/owner")]
        //[ProducesResponseType(typeof(MemoryExtended), 204)]
        //[ProducesResponseType(typeof(NotFoundResult), 404)]
        //[ProducesResponseType(typeof(StatusCodeResult), 500)]
        //public async Task<IActionResult> GetWithOwner(int id)
        //{
        //    try
        //    {
        //        var result = await _repository.Memory.GetMemoryWithOwnerAsync(id);

        //        if (result.IsEmptyObject())
        //        {
        //            _logger.LogError(string.Format(_messages.Controller["NotFound"], _entity, id));
        //            return NotFound();
        //        }
        //        else
        //        {
        //            _logger.LogError(string.Format(_messages.Controller["ReturnedDetails"], _entity, id));
        //            return Ok(result);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(string.Format(_messages.Controller["SomeError"], ex.Message));
        //        return StatusCode(500, string.Format(_messages.Controller["Error500"]));
        //    }
        //}

        /// <summary>
        /// Update a Memory
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /Memory
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
        /// <param name="memory">Memory</param>
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
        public async Task<IActionResult> UpdateMemory([FromBody]Memory memory)
        {
            try
            {
                if (memory.IsObjectNull())
                {
                    _logger.LogError(string.Format(_messages.Controller["NullObject"], _entity));
                    return BadRequest(string.Format(_messages.Controller["NullObject"], _entity));
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError(string.Format(_messages.Controller["InvalidObject"], _entity));
                    return BadRequest(string.Format(_messages.Controller["InvalidObject"], _entity));
                }

                var dbMemory = await _repository.Memory.GetMemoryByIdAsync(memory.Id);
                if (dbMemory.IsEmptyObject())
                {
                    _logger.LogError(string.Format(_messages.Controller["NotFound"], _entity, memory.Id));
                    return NotFound();
                }

                await _repository.Memory.UpdateMemoryAsync(memory.Id, memory);
                _logger.LogError(string.Format(_messages.Controller["NotFound"], _entity, memory.Id));
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format(_messages.Controller["SomeError"], ex.Message));
                return StatusCode(500, string.Format(_messages.Controller["Error500"]));
            }
        }

        /// <summary>
        /// Create a Memory
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /Memory/1
        ///     {
        ///         "name": "string",
        ///         "description": "string",
        ///         "ownerId": "string",
        ///         "id": 0,
        ///         "lastModified": "2018-08-07T01:39:19.706Z"
        ///     }
        ///
        /// </remarks>
        /// <param name="memory">Memory</param>
        /// <returns></returns>
        /// <response code="201">Object created at route</response>
        /// <response code="400">Object Null or Invalid</response>
        /// <response code="500">Internal server error</response> 
        [HttpPost]
        [ProducesResponseType(typeof(Memory), 201)]
        [ProducesResponseType(typeof(BadRequestObjectResult), 400)]
        [ProducesResponseType(typeof(StatusCodeResult), 500)]
        public async Task<IActionResult> Post([FromBody]Memory memory)
        {
            try
            {
                if (memory.IsObjectNull())
                {
                    _logger.LogError(string.Format(_messages.Controller["NullObject"], _entity));
                    return BadRequest(string.Format(_messages.Controller["NullObject"], _entity));
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError(string.Format(_messages.Controller["InvalidObject"], _entity));
                    return BadRequest(string.Format(_messages.Controller["InvalidObject"], _entity));
                }

                var Id = await _repository.Memory.CreateMemoryAsync(memory);

                var ret = CreatedAtRoute("GetMemoryById", new { id = Id }, memory);
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
        /// Delete a Memory by Id
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     DELETE /Memory/1
        ///     {
        ///      
        ///     }
        ///
        /// </remarks>
        /// <param name="id">Memory Id</param>
        /// <returns>Memory Id</returns>
        /// <response code="200">Object Result Id</response>
        /// <response code="404">Object with Id not found</response>
        /// <response code="500">Internal server error</response>     
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ObjectResult), 200)]
        [ProducesResponseType(typeof(NotFoundResult), 404)]
        [ProducesResponseType(typeof(StatusCodeResult), 500)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var memory = await _repository.Memory.GetMemoryByIdAsync(id);
                if (memory.IsEmptyObject())
                {
                    _logger.LogError(string.Format(_messages.Controller["NotFound"], _entity, id));
                    return NotFound();
                }

                await _repository.Memory.DeleteMemoryAsync(id);
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
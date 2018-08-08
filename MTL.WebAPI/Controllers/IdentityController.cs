using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MTL.DataAccess.Contracts;
using Microsoft.Extensions.Logging;
using MTL.DataAccess.Entities;
using Microsoft.Extensions.Options;
using MTL.Library.Common;
using AutoMapper;
using MTL.Library.Helpers;
using MTL.WebAPI.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace MTL.WebAPI.Controllers
{
    /// <summary>
    /// Identity controller manages all things Identity
    /// </summary>
    [Route("api/Identity")]
    [ApiController]
    public class IdentityController : ControllerBase
    {
        private readonly ILogger<IdentityController> _logger;
        private IRepositoryWrapper _repository;
        private string _entity;
        private readonly Messages _messages;
        private readonly IMapper _mapper;

        /// <summary>
        /// Identity controller manages all things Identity
        /// </summary>
        public IdentityController(IRepositoryWrapper repository, IOptions<Messages> messages, ILogger<IdentityController> logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _entity = "Identity";
            _messages = messages.Value;
            _mapper = mapper;

        }

        /// <summary>
        /// Get an Identity by Id
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     Get /Identity/1fsdf-sadf-asdf-asd
        ///     {
        ///       
        ///     }
        ///
        /// </remarks>
        /// <param name="id">Identity Id</param>
        /// <returns>A single Identity</returns>
        /// <response code="200">IdentityObject</response>
        /// <response code="404">Object with Id not found</response>
        /// <response code="500">Internal server error</response>       
        [HttpGet("{id}", Name = "GetIdentityById")]
        [ProducesResponseType(typeof(IdentityUser), 200)]
        [ProducesResponseType(typeof(NotFoundResult), 404)]
        [ProducesResponseType(typeof(StatusCodeResult), 500)]
        public async Task<IActionResult> Get(string id)
        {
            try
            {
                var result = await _repository.IdentityUsers.FindByIdAsync(id);

                if (result == null)
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
        /// Get all Identities
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /Identity
        ///     {
        ///     
        ///     }
        ///
        /// </remarks>
        /// <returns>A list of IdentityUsers</returns>
        /// <response code="200">IdentityUser[]</response>
        /// <response code="500">Internal server error</response>            
        [HttpGet]
        [ProducesResponseType(typeof(IdentityUser[]), 200)]
        [ProducesResponseType(typeof(StatusCodeResult), 500)]
        public  IActionResult GetAllIdentityUsers()
        {
            try
            {
                var result = _repository.IdentityUsers.GetIdentityUsersAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format(_messages.Controller["SomeError"], ex.Message));
                return StatusCode(500, string.Format(_messages.Controller["Error500"]));
            }
        }

        /// <summary>
        /// Update a Identity
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /Identity
        ///     {
        ///         "name": "string",
        ///         "description": "string",
        ///         "appUserId": 0,
        ///         "id": 0,
        ///     }
        /// 
        ///
        /// </remarks>
        /// <param name="identity">Identity</param>
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
        public async Task<IActionResult> UpdateIdentity([FromBody]IdentityUser identity)
        {
            try
            {
                if (identity == null)
                {
                    _logger.LogError(string.Format(_messages.Controller["NullObject"], _entity));
                    return BadRequest(string.Format(_messages.Controller["NullObject"], _entity));
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError(string.Format(_messages.Controller["InvalidObject"], _entity));
                    return BadRequest(string.Format(_messages.Controller["InvalidObject"], _entity));
                }

                var dbIdentity = await _repository.IdentityUsers.FindByIdAsync(identity.Id);
                if (dbIdentity == null)
                {
                    _logger.LogError(string.Format(_messages.Controller["NotFound"], _entity, identity.Id));
                    return NotFound();
                }

               // await _repository.IdentityUsers.(identity.Id, identity);
                _logger.LogError(string.Format(_messages.Controller["NotFound"], _entity, identity.Id));
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format(_messages.Controller["SomeError"], ex.Message));
                return StatusCode(500, string.Format(_messages.Controller["Error500"]));
            }
        }

        /// <summary>
        /// Create a Identity
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /Identity/1
        ///     {
        ///         "name": "string",
        ///         "description": "string",
        ///         "appUserId": 0,
        ///     }
        ///
        /// </remarks>
        /// <param name="identity">Identity</param>
        /// <returns></returns>
        /// <response code="201">Object created at route</response>
        /// <response code="400">Object Null or Invalid</response>
        /// <response code="500">Internal server error</response> 
        [HttpPost]
        [ProducesResponseType(typeof(Identity), 201)]
        [ProducesResponseType(typeof(BadRequestObjectResult), 400)]
        [ProducesResponseType(typeof(StatusCodeResult), 500)]
        public async Task<IActionResult> Post([FromBody]Identity identity)
        {
            try
            {
                if (identity.IsObjectNull())
                {
                    _logger.LogError(string.Format(_messages.Controller["NullObject"], _entity));
                    return BadRequest(string.Format(_messages.Controller["NullObject"], _entity));
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError(string.Format(_messages.Controller["InvalidObject"], _entity));
                    return BadRequest(string.Format(_messages.Controller["InvalidObject"], _entity));
                }

                var Id = await _repository.Identitys.CreateIdentityAsync(identity);

                var ret = CreatedAtRoute("GetIdentityById", new { id = Id }, identity);
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
        /// Delete a Identity by Id
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     DELETE /Identity/1
        ///     {
        ///      
        ///     }
        ///
        /// </remarks>
        /// <param name="id">Identity Id</param>
        /// <returns>Identity Id</returns>
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
                var identity = await _repository.Identitys.GetIdentityByIdAsync(id);
                if (identity.IsEmptyObject())
                {
                    _logger.LogError(string.Format(_messages.Controller["NotFound"], _entity, id));
                    return NotFound();
                }

                await _repository.Identitys.DeleteIdentityAsync(id);
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
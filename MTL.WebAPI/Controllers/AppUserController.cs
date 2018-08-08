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
    /// AppUser Controller Manages all things AppUser
    /// </summary>
    [Route("api/AppUser")]
    [ApiController]
    public class AppUserController : ControllerBase
    {
        private readonly ILogger<AppUserController> _logger;
        private IRepositoryWrapper _repository;
        private string _entity;
        private readonly Messages _messages;

        /// <summary>
        /// AppUser Controller Manages all things AppUser
        /// </summary>
        public AppUserController(IRepositoryWrapper repository, IOptions<Messages> messages, ILogger<AppUserController> logger)
        {
            _repository = repository;
            _logger = logger;
            _entity = "AppUser";
            _messages = messages.Value;

        }

        /// <summary>
        /// Get all AppUsers
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /AppUser
        ///     {
        ///     
        ///     }
        ///
        /// </remarks>
        /// <returns>A list of AppUsers</returns>
        /// <response code="200">AppUser[]</response>
        /// <response code="500">Internal server error</response>            
        [HttpGet]
        [ProducesResponseType(typeof(AppUser[]), 200)]
        [ProducesResponseType(typeof(StatusCodeResult), 500)]
        public async Task<IActionResult> GetAllAppUsers()
        {
            try
            {
                var result = await _repository.AppUsers.GetAllAppUsersAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format(_messages.Controller["SomeError"], ex.Message));
                return StatusCode(500, string.Format(_messages.Controller["Error500"]));
            }
        }

        /// <summary>
        /// Get AppUser by IdentityId
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /AppUser/Identity/1
        ///     {
        ///        
        ///     }
        ///
        /// </remarks>
        /// <param name="identityId">Identity Id</param>
        /// <returns>A list of AppUsers for the owner</returns>
        /// <response code="200">AppUsers[]</response>
        /// <response code="500">Internal server error</response>            
        [HttpGet("identity/{identityId}")]
        [ProducesResponseType(typeof(AppUser[]), 200)]
        [ProducesResponseType(typeof(StatusCodeResult), 500)]
        public async Task<IActionResult> GetByIdentityId(string identityId)
        {
            try
            {
                var result = await _repository.AppUsers.GetAppUsersByIdentityIdAsync(identityId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format(_messages.Controller["SomeError"], ex.Message));
                return StatusCode(500, string.Format(_messages.Controller["Error500"]));
            }
        }


        /// <summary>
        /// Get a AppUser by Id
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     Get /AppUser/1
        ///     {
        ///       
        ///     }
        ///
        /// </remarks>
        /// <param name="id">AppUser Id</param>
        /// <returns>A single AppUser</returns>
        /// <response code="200">AppUser</response>
        /// <response code="404">Object with Id not found</response>
        /// <response code="500">Internal server error</response>       
        [HttpGet("{id}", Name = "GetAppUserById")]
        [ProducesResponseType(typeof(AppUser), 200)]
        [ProducesResponseType(typeof(NotFoundResult), 404)]
        [ProducesResponseType(typeof(StatusCodeResult), 500)]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var result = await _repository.AppUsers.GetAppUserByIdAsync(id);

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
        /// Get a Extended AppUser with Details
        /// Identity User
        /// UserProfile
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     Get /AppUser/1/Details
        ///     {
        ///        
        ///     }
        ///
        /// </remarks>
        /// <param name="id">AppUser Id</param>
        /// <returns>A single extended AppUser</returns>
        /// <response code="200">ExtendedAppUser</response>
        /// <response code="404">Object with Id not found</response>
        /// <response code="500">Internal server error</response>  
        [HttpGet("{id}/details")]
        [ProducesResponseType(typeof(AppUserExtended), 204)]
        [ProducesResponseType(typeof(NotFoundResult), 404)]
        [ProducesResponseType(typeof(StatusCodeResult), 500)]
        public async Task<IActionResult> GetWithDetails(int id)
        {
            try
            {
                var result = await _repository.AppUsers.GetAppUserWithDetailsAsync(id);

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
        /// Update an AppUser
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /AppUser
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
        /// <param name="appUser">AppUser</param>
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
        public async Task<IActionResult> UpdateAppUser([FromBody]AppUser appUser)
        {
            try
            {
                if (appUser.IsObjectNull())
                {
                    _logger.LogError(string.Format(_messages.Controller["NullObject"], _entity));
                    return BadRequest(string.Format(_messages.Controller["NullObject"], _entity));
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError(string.Format(_messages.Controller["InvalidObject"], _entity));
                    return BadRequest(string.Format(_messages.Controller["InvalidObject"], _entity));
                }

                var dbAppUser = await _repository.AppUsers.GetAppUserByIdAsync(appUser.Id);
                if (dbAppUser.IsEmptyObject())
                {
                    _logger.LogError(string.Format(_messages.Controller["NotFound"], _entity, appUser.Id));
                    return NotFound();
                }

                await _repository.AppUsers.UpdateAppUserAsync(appUser.Id, appUser);
                _logger.LogError(string.Format(_messages.Controller["NotFound"], _entity, appUser.Id));
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format(_messages.Controller["SomeError"], ex.Message));
                return StatusCode(500, string.Format(_messages.Controller["Error500"]));
            }
        }

        /// <summary>
        /// Create a AppUser
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /AppUser/1
        ///     {
        ///         "name": "string",
        ///         "description": "string",
        ///         "ownerId": "string",
        ///         "id": 0,
        ///         "lastModified": "2018-08-07T01:39:19.706Z"
        ///     }
        ///
        /// </remarks>
        /// <param name="appUser">AppUser</param>
        /// <returns></returns>
        /// <response code="201">Object created at route</response>
        /// <response code="400">Object Null or Invalid</response>
        /// <response code="500">Internal server error</response> 
        [HttpPost]
        [ProducesResponseType(typeof(AppUser), 201)]
        [ProducesResponseType(typeof(BadRequestObjectResult), 400)]
        [ProducesResponseType(typeof(StatusCodeResult), 500)]
        public async Task<IActionResult> Post([FromBody]AppUser appUser)
        {
            try
            {
                if (appUser.IsObjectNull())
                {
                    _logger.LogError(string.Format(_messages.Controller["NullObject"], _entity));
                    return BadRequest(string.Format(_messages.Controller["NullObject"], _entity));
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError(string.Format(_messages.Controller["InvalidObject"], _entity));
                    return BadRequest(string.Format(_messages.Controller["InvalidObject"], _entity));
                }

                var Id = await _repository.AppUsers.CreateAppUserAsync(appUser);

                var ret = CreatedAtRoute("GetAppUserById", new { id = Id }, appUser);
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
        /// Delete a AppUser by Id
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     DELETE /AppUser/1
        ///     {
        ///      
        ///     }
        ///
        /// </remarks>
        /// <param name="id">AppUser Id</param>
        /// <returns>AppUser Id</returns>
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
                var appUser = await _repository.AppUsers.GetAppUserByIdAsync(id);
                if (appUser.IsEmptyObject())
                {
                    _logger.LogError(string.Format(_messages.Controller["NotFound"], _entity, id));
                    return NotFound();
                }

                await _repository.AppUsers.DeleteAppUserAsync(id);
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
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
    /// UserProfile Controller Manages all things UserProfile
    /// </summary>
    [Route("api/UserProfile")]
    [ApiController]
    public class UserProfileController : ControllerBase
    {
        private readonly ILogger<UserProfileController> _logger;
        private IRepositoryWrapper _repository;
        private string _entity;
        private readonly Messages _messages;

        /// <summary>
        /// UserProfile Controller Manages all things UserProfile
        /// </summary>
        public UserProfileController(IRepositoryWrapper repository, IOptions<Messages> messages, ILogger<UserProfileController> logger)
        {
            _repository = repository;
            _logger = logger;
            _entity = "UserProfile";
            _messages = messages.Value;

        }

        /// <summary>
        /// Get all UserProfiles
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /UserProfile
        ///     {
        ///     
        ///     }
        ///
        /// </remarks>
        /// <returns>A list of Timelines</returns>
        /// <response code="200">UserProfiles[]</response>
        /// <response code="500">Internal server error</response>            
        [HttpGet]
        [ProducesResponseType(typeof(UserProfile[]), 200)]
        [ProducesResponseType(typeof(StatusCodeResult), 500)]
        public async Task<IActionResult> GetAllUserProfiles()
        {
            try
            {
                var result = await _repository.UserProfile.GetAllUserProfilesAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format(_messages.Controller["SomeError"], ex.Message));
                return StatusCode(500, string.Format(_messages.Controller["Error500"]));
            }
        }

        ///// <summary>
        ///// Get UserProfile by IdentityId
        ///// </summary>
        ///// <remarks>
        ///// Sample request:
        /////
        /////     GET /UserProfile/identity/1
        /////     {
        /////        
        /////     }
        /////
        ///// </remarks>
        ///// <param name = "identityId" > Identity Id</param>
        ///// <returns>A list of UserProfiles for the identity</returns>
        ///// <response code = "200" > UserProfiles[] </ response >
        ///// < response code= "500" > Internal server error</response>            
        ////[HttpGet("identity/{identityId}")]
        ////[ProducesResponseType(typeof(UserProfile[]), 200)]
        ////[ProducesResponseType(typeof(StatusCodeResult), 500)]
        ////public async Task<IActionResult> GetAllByIdentityId(string identityId)
        ////{
        ////    try
        ////    {
        ////        var result = await _repository.UserProfile.GetUserProfileByIdentityId(identityId);
        ////        return Ok(result);
        ////    }
        ////    catch (Exception ex)
        ////    {
        ////        _logger.LogError(string.Format(_messages.Controller["SomeError"], ex.Message));
        ////        return StatusCode(500, string.Format(_messages.Controller["Error500"]));
        ////    }
        ////}


        /// <summary>
        /// Get a UserProfile by Id
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     Get /UserProfile/1
        ///     {
        ///       
        ///     }
        ///
        /// </remarks>
        /// <param name="id">UserProfile Id</param>
        /// <returns>A single UserProfile</returns>
        /// <response code="200">UserProfile</response>
        /// <response code="404">Object with Id not found</response>
        /// <response code="500">Internal server error</response>       
        [HttpGet("{id}", Name = "GetUserProfileById")]
        [ProducesResponseType(typeof(UserProfile), 200)]
        [ProducesResponseType(typeof(NotFoundResult), 404)]
        [ProducesResponseType(typeof(StatusCodeResult), 500)]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var result = await _repository.UserProfile.GetUserProfileByIdAsync(id);

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
        /// Get a UserProfile with its Memories
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     Get /UserProfile/1/Memories
        ///     {
        ///        
        ///     }
        ///
        /// </remarks>
        /// <param name="id">UserProfile Id</param>
        /// <returns>A single extended UserProfile</returns>
        /// <response code="200">ExtendedUserProfile</response>
        /// <response code="404">Object with Id not found</response>
        /// <response code="500">Internal server error</response>  
        [HttpGet("{id}/memories")]
        [ProducesResponseType(typeof(UserProfileExtended), 204)]
        [ProducesResponseType(typeof(NotFoundResult), 404)]
        [ProducesResponseType(typeof(StatusCodeResult), 500)]
        public async Task<IActionResult> GetWithDetails(int id)
        {
            try
            {
                var result = await _repository.UserProfile.GetUserProfileWithDetailsAsync(id);

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
        /// Get a UserProfile with its Identity
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     Get /UserProfile/1/Identity
        ///     {
        ///       
        ///     }
        ///
        /// </remarks>
        /// <param name="id">UserProfile Id</param>
        /// <returns>A single extended UserProfile</returns>
        /// <response code="200">ExtendedUserProfile</response>
        /// <response code="404">Object with Id not found</response>
        /// <response code="500">Internal server error</response>  
        [HttpGet("{id}/identity")]
        [ProducesResponseType(typeof(UserProfileExtended), 204)]
        [ProducesResponseType(typeof(NotFoundResult), 404)]
        [ProducesResponseType(typeof(StatusCodeResult), 500)]
        public async Task<IActionResult> GetWithIdentity(int id)
        {
            try
            {
                var result = await _repository.UserProfile.GetUserProfileWithDetailsAsync(id);

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
        /// Update a UserProfile
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /UserProfile
        ///     {
        ///         "name": "string",
        ///         "description": "string",
        ///         "identityId": "string",
        ///         "id": 0,
        ///         "lastModified": "2018-08-07T01:39:19.706Z"
        ///     }
        /// 
        ///
        /// </remarks>
        /// <param name="userProfile">UserProfile</param>
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
        public async Task<IActionResult> UpdateUserProfile([FromBody]UserProfile userProfile)
        {
            try
            {
                if (userProfile.IsObjectNull())
                {
                    _logger.LogError(string.Format(_messages.Controller["NullObject"], _entity));
                    return BadRequest(string.Format(_messages.Controller["NullObject"], _entity));
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError(string.Format(_messages.Controller["InvalidObject"], _entity));
                    return BadRequest(string.Format(_messages.Controller["InvalidObject"], _entity));
                }

                var dbUserProfile = await _repository.UserProfile.GetUserProfileByIdAsync(userProfile.Id);
                if (dbUserProfile.IsEmptyObject())
                {
                    _logger.LogError(string.Format(_messages.Controller["NotFound"], _entity, userProfile.Id));
                    return NotFound();
                }

                await _repository.UserProfile.UpdateUserProfileAsync(userProfile.Id, userProfile);
                _logger.LogError(string.Format(_messages.Controller["NotFound"], _entity, userProfile.Id));
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format(_messages.Controller["SomeError"], ex.Message));
                return StatusCode(500, string.Format(_messages.Controller["Error500"]));
            }
        }

        /// <summary>
        /// Create a UserProfile
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /UserProfile/1
        ///     {
        ///         "name": "string",
        ///         "description": "string",
        ///         "identityId": "string",
        ///         "id": 0,
        ///         "lastModified": "2018-08-07T01:39:19.706Z"
        ///     }
        ///
        /// </remarks>
        /// <param name="userProfile">UserProfile</param>
        /// <returns></returns>
        /// <response code="201">Object created at route</response>
        /// <response code="400">Object Null or Invalid</response>
        /// <response code="500">Internal server error</response> 
        [HttpPost]
        [ProducesResponseType(typeof(UserProfile), 201)]
        [ProducesResponseType(typeof(BadRequestObjectResult), 400)]
        [ProducesResponseType(typeof(StatusCodeResult), 500)]
        public async Task<IActionResult> Post([FromBody]UserProfile userProfile)
        {
            try
            {
                if (userProfile.IsObjectNull())
                {
                    _logger.LogError(string.Format(_messages.Controller["NullObject"], _entity));
                    return BadRequest(string.Format(_messages.Controller["NullObject"], _entity));
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError(string.Format(_messages.Controller["InvalidObject"], _entity));
                    return BadRequest(string.Format(_messages.Controller["InvalidObject"], _entity));
                }

                var Id = await _repository.UserProfile.CreateUserProfileAsync(userProfile);

                var ret = CreatedAtRoute("GetUserProfileById", new { id = Id }, userProfile);
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
        /// Delete a UserProfile by Id
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     DELETE /UserProfile/1
        ///     {
        ///      
        ///     }
        ///
        /// </remarks>
        /// <param name="id">UserProfile Id</param>
        /// <returns>UserProfile Id</returns>
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
                var userProfile = await _repository.UserProfile.GetUserProfileByIdAsync(id);
                if (userProfile.IsEmptyObject())
                {
                    _logger.LogError(string.Format(_messages.Controller["NotFound"], _entity, id));
                    return NotFound();
                }

                await _repository.UserProfile.DeleteUserProfileAsync(id);
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
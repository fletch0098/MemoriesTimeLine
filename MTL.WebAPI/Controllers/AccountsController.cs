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
    /// Account Controller Manages all things Account
    /// </summary>
    [Route("api/Accounts")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly ILogger<AccountController> _logger;
        private IRepositoryWrapper _repository;
        private string _entity;
        private readonly Messages _messages;
        private readonly IMapper _mapper;

        /// <summary>
        /// Account Controller Manages all things Account
        /// </summary>
        public AccountController(IRepositoryWrapper repository, IOptions<Messages> messages, ILogger<AccountController> logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _entity = "Account";
            _messages = messages.Value;
            _mapper = mapper;

        }

        /// <summary>
        /// Create a new Account
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
        /// <param name="model">RegistrationViewModel</param>
        /// <returns></returns>
        /// <response code="201">Object created at route</response>
        /// <response code="400">Object Null or Invalid</response>
        /// <response code="500">Internal server error</response> 
        [HttpPost]
        [ProducesResponseType(typeof(OkObjectResult), 20)]
        [ProducesResponseType(typeof(BadRequestObjectResult), 400)]
        [ProducesResponseType(typeof(StatusCodeResult), 500)]
        public async Task<IActionResult> Post([FromBody]RegistrationViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userIdentity = _mapper.Map<IdentityUser>(model);

            var result = await _repository.IdentityUsers.CreateIdentityUserAsync(userIdentity, model.Password);

            if (!result.Succeeded) return new BadRequestObjectResult(Errors.AddErrorsToModelState(result, ModelState));

            var appUserId = await _repository.AppUsers.CreateAppUserAsync(new AppUser { IdentityId = userIdentity.Id, FirstName = model.FirstName, LastName = model.LastName });

            await _repository.UserProfiles.CreateUserProfileAsync(new UserProfile { AppUserId = appUserId, Location = model.Location });

            return new OkObjectResult("Account created");
        }

        //    /// <summary>
        //    /// Get an Identity by Id
        //    /// </summary>
        //    /// <remarks>
        //    /// Sample request:
        //    ///
        //    ///     Get /Identity/1
        //    ///     {
        //    ///       
        //    ///     }
        //    ///
        //    /// </remarks>
        //    /// <param name="id">Identity Id</param>
        //    /// <returns>A single Identity</returns>
        //    /// <response code="200">AppUser</response>
        //    /// <response code="404">Object with Id not found</response>
        //    /// <response code="500">Internal server error</response>       
        //    [HttpGet("{id}", Name = "GetAppUserById")]
        //    [ProducesResponseType(typeof(AppUser), 200)]
        //    [ProducesResponseType(typeof(NotFoundResult), 404)]
        //    [ProducesResponseType(typeof(StatusCodeResult), 500)]
        //    public async Task<IActionResult> Get(string id)
        //    {
        //        try
        //        {
        //            var result = await _repository.AppUser.(id);

        //            if (result.IsEmptyObject())
        //            {
        //                _logger.LogError(string.Format(_messages.Controller["NotFound"], _entity, id));
        //                return NotFound();
        //            }
        //            else
        //            {
        //                _logger.LogError(string.Format(_messages.Controller["ReturnedId"], _entity, id));
        //                return Ok(result);
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            _logger.LogError(string.Format(_messages.Controller["SomeError"], ex.Message));
        //            return StatusCode(500, string.Format(_messages.Controller["Error500"]));
        //        }
        //    }
    }
}
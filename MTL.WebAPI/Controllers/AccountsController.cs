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

            var userIdentity = _mapper.Map<AppUser>(model);

            var result = await _repository.AppUser.CreateAppUserAsync(userIdentity, model.Password);

            if (!result.Succeeded) return new BadRequestObjectResult(Errors.AddErrorsToModelState(result, ModelState));

            await _repository.UserProfile.CreateUserProfileAsync(new UserProfile { IdentityId = userIdentity.Id, Location = model.Location });

            return new OkObjectResult("Account created");
        }
    }
}
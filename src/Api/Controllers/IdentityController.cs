using System;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using Application.Common;
using Application.Model.Request;
using Application.Model.Response;
using Application.Service.Interfaces;
using Application.Service.LoggerService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    public class IdentityController : ControllerBase
    {
        private readonly IIdentityService _identityService;
        private readonly ILoggerManager _logger;

        public IdentityController(IIdentityService identityService, ILoggerManager logger)
        {
            _identityService = identityService;
            _logger = logger;
        }

        /// <summary>
        /// Returns new registered account
        /// </summary>
        /// <response code="200">Returns new registered account</response>
        /// <response code="400">Some fields not in valid format</response>        
        [ProducesResponseType(typeof(UserRes), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost("api/[action]")]
        public async Task<IActionResult> Register([FromBody] UserReq request)
        {
            //generic model validation added
            //if (!ModelState.IsValid)
            //    return BadRequest(Result.Failure(ModelState.Values.SelectMany(x => x.Errors.Select(xx => xx.ErrorMessage))));
            //
            var response = await _identityService.RegisterAsync(request);
            if (!response.Succeeded)
            {
                _logger.LogWarn($"Register email:{request.EmailAddress}{Environment.NewLine}{string.Join(",", response.Messages)}");
                return BadRequest(response);
            }
            //
            _logger.LogInfo($"Registere email:{request.EmailAddress}{Environment.NewLine}{string.Join(",", response.Messages)}");
            return Ok(response);

        }

        /// <summary>
        /// Returns new registered account
        /// </summary>
        /// <response code="200">Returns new registered account</response>
        /// <response code="400">Some fields not in valid format</response>        
        /// <response code="404">There is no data</response>        
        [ProducesResponseType(typeof(LoginRes), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPost("api/[action]")]        
        public async Task<IActionResult> Login([FromBody] LoginReq request)
        {
            var response = await _identityService.LoginAsync(request);
            if (!response.Succeeded)
            {
                _logger.LogInfo($"Login - {request.EmailAddress ?? request.Username}:{Environment.NewLine}{string.Join(",", response.Messages)}");
                return NotFound(Result.Failure(response.Messages));
            }
            //
            _logger.LogInfo($"Login - email:{request.EmailAddress}{Environment.NewLine}{string.Join(",", response.Messages)}");
            return Ok(response);
        }

    }
}

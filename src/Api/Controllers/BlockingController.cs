using System.Linq;
using System.Threading.Tasks;
using Api.Extensions;
using Application.Common;
using Application.Model.Request;
using Application.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Produces("application/json")]
    public class BlockingController : CustomApiController
    {
        private readonly IBlockingService _blockingService;
        private readonly ICurrentUserService _currentUser;

        public BlockingController(IBlockingService blockingService, ICurrentUserService currentUser)
        {
            _blockingService = blockingService;
            _currentUser = currentUser;
        }

        /// <summary>
        /// Returns true if user will blocked
        /// </summary>
        /// <response code="200">Returns true if user will blocked</response>
        /// <response code="400">Some fields not in valid format</response>        
        [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
        [HttpPost("api/block-user")]
        public async Task<IActionResult> BlockUser([FromBody] BlockUserReq request)
        {
            //            
            var response = await _blockingService.BlockUserAsync(_currentUser.UserId, request);
            if (!response.Succeeded)
                return BadRequest(Result.Failure(response.Messages));
            //
            return Ok(response);
        }

        /// <summary>
        /// Returns true if user block will remove
        /// </summary>
        /// <response code="200">Returns user block from blocking list</response>
        /// <response code="400">Some fields not in valid format</response>        
        /// [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
        [HttpDelete("api/block-remove")]
        public async Task<IActionResult> BlockRemoval([FromBody] BlockUserReq request)
        {
            var response = await _blockingService.BlockRemoveAsync(_currentUser.UserId, request);
            if (!response.Succeeded)
                return BadRequest(Result.Failure(response.Messages));
            //
            return Ok(response);
        }
    }
}

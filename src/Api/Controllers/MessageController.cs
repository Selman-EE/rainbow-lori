using Api.Extensions;
using Application.Common;
using Application.Model.Request;
using Application.Model.Response;
using Application.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Api.Controllers
{
    public class MessageController : CustomApiController
    {
        private readonly IMessageService _messageService;
        private readonly ICurrentUserService _currentUser;

        public MessageController(IMessageService messageService, ICurrentUserService currentUser)
        {
            _messageService = messageService;
            _currentUser = currentUser;
        }

        /// <summary>
        /// start conversation
        /// </summary>
        /// <response code="200">Returns new chat id</response>
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [HttpGet("api/start-chat")]
        public async Task<IActionResult> StartChat()
        {
            var response = await _messageService.StartChat(_currentUser.UserId);
            if (!response.Succeeded)
                return BadRequest(response);

            return Ok(response);
        }

        /// <summary>
        /// old message in chat
        /// </summary>
        /// <response code="200">Returns old messages</response>
        [ProducesResponseType(typeof(MessageRes), StatusCodes.Status200OK)]
        [HttpGet("api/messages/{chatId}/{page}/{size}")]
        public async Task<IActionResult> GetMessages(string chatId, int page, int size)
        {
            size = size > 50 ? 50 : size;
            var response = await _messageService.GetMessages(chatId, page, size);
            return Ok(response);
        }

        /// <summary>
        /// send message in chat
        /// </summary>
        /// <response code="200">Returns true if message sent it</response>
        [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
        [HttpPost("api/{chatId}/send-messages")]
        public async Task<IActionResult> SendMessage(string chatId, MessageReq request)
        {
            var response = await _messageService.SendMessage(_currentUser.UserId, chatId, request.Username, request.Text);
            if (!response.Succeeded)
                return BadRequest(response);

            return Ok(response);
        }

    }
}

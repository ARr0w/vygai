using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vyg.Assessment.BE.Dtos;
using Vyg.Assessment.BE.Services.Contracts;

namespace Vyg.Assessment.BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessagingController : ControllerBase
    {
        private readonly IMessagingService _messagingService;

        public MessagingController(IMessagingService messagingService)
        {
            _messagingService = messagingService;
        }

        [HttpPost]
        [Route("sendSms")]
        [Authorize]
        public async Task<IActionResult> SendSmsAsync(MessageDto messageDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _messagingService.SendMessageAsync(messageDto);

            return Ok(new { message = result });
        }
    }
}

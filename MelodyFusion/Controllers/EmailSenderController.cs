using MelodyFusion.BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MelodyFusion.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [AllowAnonymous]
    public class EmailSenderController : ControllerBase
    {
        private readonly IEmailSender _emailSender;

        public EmailSenderController(IEmailSender emailSender)
        {
            _emailSender = emailSender;
        }

        [HttpPost]
        public IActionResult Send()
        {
            _emailSender.SendEmailAsync("artem.pushkar@nure.ua", "TestSubject", "TestBody");
            return Ok("Success");
        }
    }
}

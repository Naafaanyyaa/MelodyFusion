using MelodyFusion.BLL.Interfaces;
using MelodyFusion.BLL.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Braintree;
using MelodyFusion.BLL.Models.Request;
using MelodyFusion.BLL.Models.Response.Statistic;

namespace MelodyFusion.Controllers
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class StatisticController : ControllerBase
    {
        private readonly IStatisticService _statisticService;

        public StatisticController(IStatisticService statisticService)
        {
            _statisticService = statisticService;
        }

        [HttpGet("[action]")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(IEnumerable<SubscriptionStatisticResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetUserInfo([FromQuery] StatisticRequest request)
        {
            var result = await _statisticService.GetSubscriptionStatisticAsync(request);
            return Ok(result);
        }

        [HttpGet("[action]")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(IEnumerable<LoginStatisticResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetLoginInfo([FromQuery] StatisticRequest request)
        {
            var result = await _statisticService.GetLoginStatistic(request);
            return Ok(result);
        }
    }
}

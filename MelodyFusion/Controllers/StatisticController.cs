using MelodyFusion.BLL.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MelodyFusion.BLL.Models.Request;
using MelodyFusion.BLL.Models.Response.Statistic;

namespace MelodyFusion.Controllers
{
    /// <summary>
    /// Controller for handling statistic operations.
    /// </summary>
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class StatisticController : ControllerBase
    {
        private readonly IStatisticService _statisticService;


        /// <summary>
        /// Initializes a new instance of the <see cref="StatisticController"/> class.
        /// </summary>
        /// <param name="statisticService">The service for statistic operations.</param>
        public StatisticController(IStatisticService statisticService)
        {
            _statisticService = statisticService;
        }

        /// <summary>
        /// Retrieves subscription statistics.
        /// </summary>
        /// <param name="request">The request containing criteria for statistic retrieval.</param>
        /// <returns>An IActionResult representing the subscription statistics.</returns>
        [HttpGet("[action]")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(IEnumerable<SubscriptionStatisticResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetUserInfo([FromQuery] StatisticRequest request)
        {
            var result = await _statisticService.GetSubscriptionStatisticAsync(request);
            return Ok(result);
        }

        /// <summary>
        /// Retrieves login statistics.
        /// </summary>
        /// <param name="request">The request containing criteria for statistic retrieval.</param>
        /// <returns>An IActionResult representing the login statistics.</returns>
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

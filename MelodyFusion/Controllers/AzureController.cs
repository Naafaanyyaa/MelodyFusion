using MelodyFusion.BLL.Interfaces;
using MelodyFusion.BLL.Models.Response;
using MelodyFusion.BLL.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MelodyFusion.Controllers
{
    [Route("api/[controller]")]
    [AllowAnonymous]
    [ApiController]
    public class AzureController : ControllerBase
    {

        private readonly IAzureBlobService _azureBlobService;

        public AzureController(IAzureBlobService azureBlobService)
        {
            _azureBlobService = azureBlobService;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetUserInfo()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            //await _azureBlobService.ListBlobContainersAsync();
            return Ok();
        }
    }
}

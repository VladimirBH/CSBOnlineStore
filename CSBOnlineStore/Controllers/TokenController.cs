using CSBOnlineStore.Services;
using Microsoft.AspNetCore.Mvc;
using System.Security.Authentication;
using System.Text.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CSBOnlineStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly UserService _userService;
        public TokenController(UserService userService) 
        {
            _userService = userService;
        }
        [HttpGet]
        public ActionResult<JsonDocument> RefreshAccess()
        {
            var httpContext = new HttpContextAccessor();
            var refreshToken = httpContext.HttpContext.Request.Headers.Authorization.ToString().Replace("Bearer ", "");
            try
            {
                var jsonString = JsonSerializer.Serialize(_userService.RefreshPairTokens(refreshToken));
                var json = JsonDocument.Parse(jsonString);
                return json;
            }
            catch (AuthenticationException ex)
            {
                return StatusCode(403);
            }

        }
    }
}

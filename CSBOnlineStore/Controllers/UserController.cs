using CSBOnlineStore.DataBase.Models;
using CSBOnlineStore.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Authentication;
using System.Text.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CSBOnlineStore.Controllers
{
    [Authorize(Roles = "admin")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;
        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public List<User> Get()
        {
            return _userService.GetAllWithForeignKey();
        }

        [HttpGet("{id}")]
        public User Get(int id)
        {
            return _userService.GetById(id);
        }

        [HttpGet("{username}")]
        public User GetByUsername(string username)
        {
            return _userService.GetByLogin(username);
        }


        [AllowAnonymous]
        [Authorize]
        [HttpGet]
        public ActionResult<JsonDocument> GetCurrentUserInfo()
        {
            try
            {
                var httpContext = new HttpContextAccessor();
                var accessToken = httpContext.HttpContext.Request.Headers.Authorization.ToString().Replace("Bearer ", "");
                var jsonString = JsonSerializer.Serialize(_userService.GetCurrentUserInfo(accessToken));
                var json = JsonDocument.Parse(jsonString);
                return json;
            }
            catch (AuthenticationException ex)
            {
                return StatusCode(401);
            }
        }


        [AllowAnonymous]
        [HttpPost]
        public ActionResult<JsonDocument> SignIn(Login login) 
        {
            var tokenPair = _userService.AuthenticateUser(login);
            if (tokenPair == null)
            {
                return Unauthorized("Неверный логин/пароль");
            }
            var jsonString = JsonSerializer.Serialize(tokenPair);
            var json = JsonDocument.Parse(jsonString);
            return json;
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult SignUp(User user)
        {
            var newUser = _userService.RegisterUser(user);
            if (newUser == null)
            {
                return Conflict("Пользователь с таким именем уже существует");
            }
            //var tokenPair = _userService.AuthenticateUser(new Login() { Username = user.Username, Password = user.Password });
            //var jsonString = JsonSerializer.Serialize(tokenPair);
            //var json = JsonDocument.Parse(jsonString);
            return Ok("Пользователь успешно зарегистрирован");
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _userService.DeleteUser(id);
        }
    }
}

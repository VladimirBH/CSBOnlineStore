using CSBOnlineStore.Classes;
using CSBOnlineStore.DataBase.Models;
using CSBOnlineStore.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CSBOnlineStore.Controllers
{
    [Authorize]
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
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }


        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        [HttpPost]
        public void Post([FromBody] string value)
        {
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
        public ActionResult<JsonDocument> SignUp(User user)
        {
            var newUser = _userService.RegisterUser(user);
            if (newUser == null)
            {
                return Conflict("Пользователь с таким именем уже существует");
            }
            var tokenPair = _userService.AuthenticateUser(new Login() { Username = user.Username, Password = user.Password });
            var jsonString = JsonSerializer.Serialize(tokenPair);
            var json = JsonDocument.Parse(jsonString);
            return json;
        }

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

using CSBOnlineStore.Classes;
using CSBOnlineStore.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CSBOnlineStore.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly CartService _cartService;
        private readonly UserService _userService;
        public CartController(CartService cartService, UserService userService)
        {
            _cartService = cartService;
            _userService = userService;
        }
        // GET: api/<ValuesController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ValuesController>
        [HttpGet]
        public void AddToCart(int productId)
        {
            var httpContext = new HttpContextAccessor();
            var accessToken = httpContext.HttpContext.Request.Headers.Authorization.ToString().Replace("Bearer ", "");
            int userId = _userService.GetUserIdFromAccessToken(accessToken);
            CartDataModel cartDataModel = new CartDataModel()
            {
                ProductId = productId,
                UserId = userId,
                ProductCount = 1
            };
            _cartService.AddProductToCart(cartDataModel);
        }

        [HttpGet]
        public void RemoveFromCart(int productId)
        {
            var httpContext = new HttpContextAccessor();
            var accessToken = httpContext.HttpContext.Request.Headers.Authorization.ToString().Replace("Bearer ", "");
            int userId = _userService.GetUserIdFromAccessToken(accessToken);
            CartDataModel cartDataModel = new CartDataModel()
            {
                ProductId = productId,
                UserId = userId,
                ProductCount = 1
            };
            _cartService.RemoveProductFromCart(cartDataModel);
        }
    }
}

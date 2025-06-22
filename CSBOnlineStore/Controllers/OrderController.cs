using CSBOnlineStore.Classes;
using CSBOnlineStore.DataBase.Models;
using CSBOnlineStore.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CSBOnlineStore.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private static OrderService _orderService;
        private static UserService _userService;

        public OrderController(OrderService orderService, UserService userService) 
        {
            _orderService = orderService;
            _userService = userService;
        }

        [HttpGet]
        public List<Order> GetOrdersHistory()
        {
            var httpContext = new HttpContextAccessor();
            var accessToken = httpContext.HttpContext.Request.Headers.Authorization.ToString().Replace("Bearer ", "");
            var userId = _userService.GetCurrentUserInfo(accessToken).Id;
            return _orderService.GetUserOrder(userId);
        }

        [HttpPost]
        public ActionResult<Order> CreateOrder(OrderCreate orderCreate)
        {
            var httpContext = new HttpContextAccessor();
            var accessToken = httpContext.HttpContext.Request.Headers.Authorization.ToString().Replace("Bearer ", "");
            var userId = _userService.GetCurrentUserInfo(accessToken).Id;
            orderCreate.UserId = userId;
            return _orderService.CreateOrder(orderCreate);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

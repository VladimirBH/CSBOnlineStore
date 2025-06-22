using CSBOnlineStore.DataBase.Models;
using CSBOnlineStore.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;


namespace CSBOnlineStore.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class FavoriteController : ControllerBase
    {
        private static UserService _userService;
        private static FavoriteService _favoriteService;

        public FavoriteController(UserService userService, FavoriteService favoriteService)
        {
            _userService = userService;
            _favoriteService = favoriteService;
        }
        [HttpGet]
        public List<Product> GetFavoriteProducts()
        {
            var httpContext = new HttpContextAccessor();
            var accessToken = httpContext.HttpContext.Request.Headers.Authorization.ToString().Replace("Bearer ", "");
            var userId = _userService.GetCurrentUserInfo(accessToken).Id;
            return _favoriteService.GetFavoritesProductsFromUserId(userId);

        }

        [HttpGet]
        public ActionResult AddFavoriteProduct(int productId)
        {
            var httpContext = new HttpContextAccessor();
            var accessToken = httpContext.HttpContext.Request.Headers.Authorization.ToString().Replace("Bearer ", "");
            var userId = _userService.GetCurrentUserInfo(accessToken).Id;
            _favoriteService.AddToFavorite(productId, userId);

            return Ok();
        }

        [HttpGet]
        public ActionResult RemoveFavoriteProduct(int productId)
        {
            var httpContext = new HttpContextAccessor();
            var accessToken = httpContext.HttpContext.Request.Headers.Authorization.ToString().Replace("Bearer ", "");
            var userId = _userService.GetCurrentUserInfo(accessToken).Id;
            _favoriteService.DeleteFromFavorite(productId, userId);

            return Ok();
        }
    }
}

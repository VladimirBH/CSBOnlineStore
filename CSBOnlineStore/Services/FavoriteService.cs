using CSBOnlineStore.DataBase;
using CSBOnlineStore.DataBase.Models;

namespace CSBOnlineStore.Services
{
    public class FavoriteService
    {
        private static CSBContext _context;
        private readonly IConfiguration _configuration;
        public FavoriteService(CSBContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public void AddToFavorite(int productId, int userId) 
        {
            var favorite = _context.Favorites.FirstOrDefault(x => x.ProductId == productId && x.UserId == userId);
            if (favorite != null) 
            {
                throw new InvalidOperationException("Товар уже добавлен в избранное");
            }
            _context.Favorites.Add(new Favorite 
            { 
                UserId = userId,
                ProductId = productId
            });
            _context.SaveChanges();
        }

        public void DeleteFromFavorite(int productId, int userId)
        {
            var favorite = _context.Favorites.FirstOrDefault(x => x.ProductId == productId && x.UserId == userId);
            if (favorite == null)
            {
                throw new InvalidOperationException("Такого товара нет в избранном");
            }
            _context.Favorites.Remove(favorite);
            _context.SaveChanges();
        }

        public List<Product> GetFavoritesProductsFromUserId(int userId)
        {
            var products = from product in _context.Products
                           join favorite in _context.Favorites on product.Id equals favorite.UserId
                           join user in _context.Users on favorite.UserId equals user.Id
                           where user.Id == userId
                           select product;

            return products.ToList();
        }
    }
}

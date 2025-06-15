using CSBOnlineStore.Classes;
using CSBOnlineStore.DataBase;
using CSBOnlineStore.DataBase.Models;

namespace CSBOnlineStore.Services
{
    public class CartService
    {
        private static CSBContext _context;
        private readonly IConfiguration _configuration;
        public CartService(CSBContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public void AddProductToCart(CartDataModel cartDataModel) 
        {
            var cartProduct = _context.CartProducts.FirstOrDefault(
                cp => cp.ProductId == cartDataModel.Product.Id && 
                cp.UserId == cartDataModel.User.Id);

            if (cartProduct != null)
            {
                cartProduct.Quantity += cartDataModel.ProductCount;
                _context.CartProducts.Update(cartProduct);
                _context.SaveChanges();
                return;
            }
            var newCartProduct = new CartProduct()
            {
                ProductId = cartDataModel.Product.Id,
                UserId = cartDataModel.User.Id,
                Quantity = cartDataModel.ProductCount
            };
            _context.CartProducts.Add(newCartProduct);
            _context.SaveChanges();
        }

        public void RemoveProductFromCart(CartDataModel cartDataModel)
        {
            var cartProduct = _context.CartProducts.FirstOrDefault(
                cp => cp.ProductId == cartDataModel.Product.Id &&
                cp.UserId == cartDataModel.User.Id);

            if (cartProduct != null)
            {
                _context.CartProducts.Remove(cartProduct);
                _context.SaveChanges();
                return;
            }
            else 
            {
                throw new InvalidOperationException("Такого товара в корзине нету");
            }
        }

        public void SubtractProductFromCart(CartDataModel cartDataModel)
        {
            var cartProduct = _context.CartProducts.FirstOrDefault(
                cp => cp.ProductId == cartDataModel.Product.Id &&
                cp.UserId == cartDataModel.User.Id);

            if (cartProduct != null)
            {
                cartProduct.Quantity -= cartDataModel.ProductCount;
                _context.CartProducts.Update(cartProduct);
                _context.SaveChanges();
                return;
            }
            else
            {
                throw new InvalidOperationException("Такого товара в корзине нету");
            }
        }

    }
}

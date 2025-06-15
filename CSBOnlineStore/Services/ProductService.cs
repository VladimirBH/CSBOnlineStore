using CSBOnlineStore.DataBase;
using CSBOnlineStore.DataBase.Models;

namespace CSBOnlineStore.Services
{
    public class ProductService
    {
        private static CSBContext _context;
        private readonly IConfiguration _configuration;
        public ProductService(CSBContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public Product GetProductById(int id) 
        {
            return _context.Products.FirstOrDefault(p => p.Id == id);
        }

        public Product GetProductByArticle(string article)
        {
            return _context.Products.FirstOrDefault(p => p.Article == article);
        }

        public List<Product> GetProducts() 
        {
            return _context.Products.ToList();
        }

        public List<Product> GetProductsByCategory(string categoryName) 
        {
            var category = _context.Categories.FirstOrDefault(c => c.Name == categoryName);
            return _context.Products.Where(p => p.CategoryId == category.Id).ToList();
        }

        public void Add(Product product) 
        {
            _context.Products.Add(product);
        }

        public void Remove(int id)
        {
            var product = _context.Products.FirstOrDefault(p => p.Id == id);
            if (product != null)
            {
                _context.Products.Remove(product);
            }
            else 
            {
                throw new InvalidOperationException("Такого товара нет");
            }
            
        }

        public void Update(Product product)
        {
            _context.Products.Update(product);
        }
    }
}

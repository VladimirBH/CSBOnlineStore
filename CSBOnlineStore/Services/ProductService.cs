using CSBOnlineStore.Classes;
using CSBOnlineStore.DataBase;
using CSBOnlineStore.DataBase.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

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

        public List<Product> GetProductsByCategory(int idCat) 
        {
            var category = _context.Categories.FirstOrDefault(c => c.Id == idCat);
            return _context.Products.Where(p => p.CategoryId == category.Id).ToList();
        }

        public List<Product> GetProductsByFilter(ProductFilterDto dto)
        {
            var query = from product in _context.Products
                        join productSpet in _context.SpetificationProducts on product.Id equals productSpet.ProductId
                        join spets in _context.Spetifications on productSpet.SpetificationId equals spets.Id
                        where product.CategoryId == dto.CategoryId
                        select new
                        {
                            Product = product,
                            Attribute = spets,
                            Value = productSpet
                        };

            bool filResBool = false;
            int filResInt = 0;
            bool valBool = false;
            int valInt = 0;
            foreach (var filter in dto.Filters)
            {
                query = query.Where(x => x.Attribute.Name == filter.Name &&
                (
                    (filter.Type == DataTypeSpet.String && x.Value.Value == filter.Value) ||
                    (filter.Type == DataTypeSpet.Bool &&
                        bool.TryParse(filter.Value ?? string.Empty, out filResBool) &&
                        bool.TryParse(x.Value.Value, out valBool) &&
                        valBool == filResBool) ||
                    (filter.Type == DataTypeSpet.Number &&
                        (int.TryParse(filter.Value ?? string.Empty, out filResInt) && int.TryParse(x.Value.Value, out valInt) && valInt == filResInt) ||
                        ((filter.Min.HasValue && (int.TryParse(x.Value.Value, out valInt) && valInt >= filter.Min) &&
                        (filter.Max.HasValue && int.TryParse(x.Value.Value, out valInt) && valInt <= filter.Max))))
                ));
            }
            List<Product> products = (List<Product>)(from q in query
                    select new
                    {
                        q.Product
                    }.Product);
            return products;
        }


        public List<Product> GetProductsBySearch(string parameter) 
        {
            return _context.Products.Where(p => p.Name.ToLower().Contains(parameter) || p.Article.Contains(parameter)).ToList();
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

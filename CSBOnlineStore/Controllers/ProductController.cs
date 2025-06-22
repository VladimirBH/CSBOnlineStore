using CSBOnlineStore.Classes;
using CSBOnlineStore.DataBase.Models;
using CSBOnlineStore.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace CSBOnlineStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProductService _productService;

        public ProductController(ProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("{id}")]
        public ProductFull GetProductInfo(int id)
        {
            return _productService.GetFullProductInfo(id);
        }

        [HttpGet]
        public List<Product> Get()
        {
            return _productService.GetAllProducts();
        }

        [HttpGet]
        public List<Product> GetFilteredProducts(ProductFilter productFilter)
        {
            return _productService.GetProductsByFilter(productFilter);
        }


        [HttpGet]
        public List<Product> SearchProducts(string parameter) 
        {
            return _productService.GetProductsBySearch(parameter);
        }
    }
}

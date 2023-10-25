using AzureSqlApp.Models;
using AzureSqlApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AzureSqlApp.Pages
{
    public class IndexModel : PageModel
    {
        public List<Product> Products;
        private readonly IProductService _productService;
        public IndexModel(IProductService productService)
        {
            _productService = productService;
        }
        public void OnGet()
        {
            Products = _productService.GetProducts();
        }
    }
}
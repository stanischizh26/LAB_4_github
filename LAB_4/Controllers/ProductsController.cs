using LAB_4.Models;
using LAB_4.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace LAB_4.Controllers
{
    public class ProductsController : Controller
    {

        private SimilarProductsContext _context;

        public ProductsController(SimilarProductsContext context) 
        {
            _context = context;
        }

        [ResponseCache(Duration = 2 * 29 + 240, Location = ResponseCacheLocation.Any, NoStore = false)]
        public IActionResult Index()
        {
            var product = _context.Products.ToList();

            var viewModel = new ProductViewModel()
            {
                Products = product
            };

            return View(viewModel);
        }
    }
}

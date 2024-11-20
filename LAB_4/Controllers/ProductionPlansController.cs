using LAB_4.Models;
using LAB_4.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace LAB_4.Controllers
{
    public class ProductionPlansController : Controller
    {

        private SimilarProductsContext _context;

        public ProductionPlansController(SimilarProductsContext context) 
        {
            _context = context;
        }

        [ResponseCache(Duration = 2 * 29 + 240, Location = ResponseCacheLocation.Any, NoStore = false)]
        public IActionResult Index()
        {
            var productionPlan = _context.ProductionPlans.Include(p => p.Product).Include(e => e.Enterprise).ToList();

            var viewModel = new ProductionPlanViewModel()
            {
                ProductionPlans = productionPlan
            };

            return View(viewModel);
        }
    }
}

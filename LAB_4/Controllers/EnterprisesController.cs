using LAB_4.Models;
using LAB_4.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace LAB_4.Controllers
{
    public class EnterprisesController : Controller
    {

        private SimilarProductsContext _context;

        public EnterprisesController(SimilarProductsContext context) 
        {
            _context = context;
        }

        [ResponseCache(Duration = 2 * 29 + 240, Location = ResponseCacheLocation.Any, NoStore = false)]
        public IActionResult Index() 
        {
            var enterprise = _context.Enterprises.ToList();

            var viewModel = new EnterpriseViewModel()
            {
                Enterprises = enterprise
            };

            return View(viewModel);
        }
    }
}

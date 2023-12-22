using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using WebApplication1.Areas.Admin.Models;
using WebApplication1.Data;
using WebApplication1.Models;
using X.PagedList;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _dbContext;
        public HomeController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index(int page)
        {
            var product = _dbContext.Products.Include(x => x.ProductImage).Include(x => x.Category).ToList();

            var model = new ProductIndexVM
            {
                Products = product
            };

            return View(model);
        }
    }
}
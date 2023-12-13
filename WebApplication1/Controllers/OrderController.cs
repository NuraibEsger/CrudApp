using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class OrderController : Controller
    {
        private readonly AppDbContext _dbContext;
        public OrderController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IActionResult Index()
        {
            var orders = _dbContext.Orders
                .Include(x=>x.Product)
                .Include(x=>x.Customer)
                .ToList();
            var model = new OrderIndexVM()
            {
                Orders = orders,
            };

            return View(model);
        }
    }
}

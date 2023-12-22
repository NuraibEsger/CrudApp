using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Areas.Admin.Models;
using WebApplication1.Data;
using WebApplication1.Entities;

namespace WebApplication1.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
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
                .Include(x => x.Product)
                .Include(x => x.Customer)
                .ToList();
            var model = new OrderIndexVM()
            {
                Orders = orders,
            };

            return View(model);
        }
        public IActionResult Add()
        {
            var model = new OrderAddVM();

            var product = _dbContext.Products.ToList();
            var customer = _dbContext.Customers.ToList();

            model.Products = product.Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString()
            }).ToList();

            model.Customers = customer.Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString()
            }).ToList();

            return View(model);
        }
        [HttpPost]
        public IActionResult Add(OrderAddVM model)
        {
            if (!ModelState.IsValid)
            {
                var product = _dbContext.Products.ToList();
                var customer = _dbContext.Customers.ToList();

                model.Products = product.Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                }).ToList();

                model.Customers = customer.Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                }).ToList();

                return View(model);
            }

            var order = new Order
            {
                CustomerId = model.CustomerId,
                ProductId = model.ProductId
            };

            _dbContext.Orders.Add(order);
            _dbContext.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            var order = _dbContext.Orders.FirstOrDefault(x => x.Id == id);
            if (order is null) return NotFound();

            _dbContext.Orders.Remove(order);
            _dbContext.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Update(int id)
        {
            var orders = _dbContext.Orders
                .Include(x => x.Customer)
                .Include(x => x.Product)
                .FirstOrDefault(x => x.Id == id);

            if (orders is null) return NotFound();

            var customers = _dbContext.Customers.ToList();
            var products = _dbContext.Products.ToList();

            var model = new OrderUpdateVM
            {
                Customers = customers.Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString(),
                }).ToList(),

                CustomerId = orders.CustomerId,

                Products = products.Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString(),
                }).ToList(),

                ProductId = orders.ProductId,
            };
            return View(model);
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Update(OrderUpdateVM model)
        {
            if (!ModelState.IsValid) return View(model);

            var orders = _dbContext.Orders.FirstOrDefault(x => x.Id == model.Id);

            if (orders is null) return NotFound();

            orders.ProductId = model.ProductId;
            orders.CustomerId = model.CustomerId;

            _dbContext.Orders.Update(orders);
            _dbContext.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}

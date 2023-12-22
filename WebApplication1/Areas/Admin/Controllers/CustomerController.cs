using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Areas.Admin.Models;
using WebApplication1.Data;
using WebApplication1.Entities;

namespace WebApplication1.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class CustomerController : Controller
    {
        private readonly AppDbContext _dbContext;
        public CustomerController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IActionResult Index()
        {
            var customers = _dbContext.Customers.ToList();

            var model = new CustomerIndexVM
            {
                Customers = customers,
            };

            return View(model);
        }
        public IActionResult Add()
        {
            var model = new CustomerAddVM();

            return View(model);
        }
        [HttpPost]
        public IActionResult Add(CustomerAddVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (_dbContext.Customers.Any(x => x.Email == model.Email))
            {
                ModelState.AddModelError("Email", "Email already exists");
                return View(model);
            }

            var customers = new Customer
            {
                Name = model.Name,
                Surname = model.Surname,
                Email = model.Email,
            };

            _dbContext.Customers.Add(customers);
            _dbContext.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Update(int id)
        {
            var customers = _dbContext.Customers.FirstOrDefault(x => x.Id == id);
            if (customers is null) return NotFound();

            var model = new CustomerUpdateVM()
            {
                Name = customers.Name,
                Surname = customers.Surname,
                Email = customers.Email,
            };

            return View(model);
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Update(CustomerUpdateVM model)
        {
            if (model is null) return NotFound();

            var customers = _dbContext.Customers.FirstOrDefault(x => x.Id == model.Id);
            if (customers is null) return NotFound();

            customers.Name = model.Name;
            customers.Surname = model.Surname;
            customers.Email = model.Email;

            _dbContext.Customers.Update(customers);
            _dbContext.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            var model = _dbContext.Customers.FirstOrDefault(x => x.Id == id);
            if (model is null) return NotFound();

            _dbContext.Customers.Remove(model);
            _dbContext.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Areas.Admin.Models;
using WebApplication1.Data;
using WebApplication1.Entities;
using WebApplication1.Services;

namespace WebApplication1.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class ProductController : Controller
    {
        private readonly AppDbContext _dbContext;
        private readonly FileService _fileService;
        public ProductController(AppDbContext dbContext, FileService fileService)
        {
            _dbContext = dbContext;
            _fileService = fileService;
        }
        public IActionResult Index()
        {
            var product = _dbContext.Products.Include(x => x.ProductImage).Include(x => x.Category).ToList();

            var model = new ProductIndexVM
            {
                Products = product,
            };

            return View(model);
        }
        public IActionResult Add()
        {
            var model = new ProductAddVM();

            var categories = _dbContext.Categories.ToList();

            model.Categories = categories.Select(x => new SelectListItem
            {
                Text = x.CategoryName,
                Value = x.Id.ToString()
            }).ToList();

            return View(model);
        }
        [HttpPost]
        public IActionResult Add(ProductAddVM model)
        {
            if (!ModelState.IsValid)
            {
                var categories = _dbContext.Categories.ToList();
                model.Categories = categories.Select(x => new SelectListItem
                {
                    Text = x.CategoryName,
                    Value = x.Id.ToString()
                }).ToList();
                return View(model);
            }

            var imageName = _fileService.UploadFile(model.Photo!);

            var product = new Product
            {
                Name = model.Name,
                Price = model.Price,
                CategoryId = model.CategoryId,
                ProductImage = new ProductImage
                {
                    ImageName = imageName,
                }
            };

            _dbContext.Products.Add(product);
            _dbContext.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            var product = _dbContext.Products.FirstOrDefault(x => x.Id == id);

            if (product is null) NotFound();

            _dbContext.Products.Remove(product!);
            _dbContext.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Update(int id)
        {
            var product = _dbContext.Products
                .Include(x => x.Category)
                .Include(x => x.ProductImage)
                .FirstOrDefault(x => x.Id == id);

            if (product is null) return NotFound();

            var categories = _dbContext.Categories.ToList();

            var model = new ProductUpdateVM
            {
                Name = product.Name,
                Price = product.Price,
                CategoryId = product.Category!.Id,
                ImageName = product.ProductImage?.ImageName,
                Categories = categories.Select(x => new SelectListItem
                {
                    Text = x.CategoryName,
                    Value = x.Id.ToString(),
                }).ToList(),
            };
            return View(model);
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Update(ProductUpdateVM model)
        {
            if (!ModelState.IsValid) return View(model);

            var product = _dbContext.Products.Include(x => x.ProductImage).FirstOrDefault(x => x.Id == model.Id);
            if (product is null) return NotFound();

            if (model.Photo != null)
            {
                if (product.ProductImage != null)
                {
                    _fileService.DeleteFile(product.ProductImage.ImageName!);
                }
                product.ProductImage!.ImageName = _fileService.UploadFile(model.Photo);
            }
            product.Name = model.Name;
            product.Price = model.Price;
            product.CategoryId = model.CategoryId;

            _dbContext.Products.Update(product);
            _dbContext.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}

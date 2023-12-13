﻿using Microsoft.AspNetCore.Mvc;
using WebApplication1.Data;
using WebApplication1.Entities;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class CategoryController : Controller
    {
        private readonly AppDbContext? _dbContext;
        public CategoryController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IActionResult Index()
        {
            var category = _dbContext.Categories.ToList();
            var model = new CategoryIndexVM
            {
                Categories = category
            };

            return View(model);
        }
        public IActionResult Add()
        {
            var model = new CategoryAddVM();

            return View(model);
        }
        [HttpPost]
        public IActionResult Add(CategoryAddVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (_dbContext.Categories.Any(c => c.CategoryName == model.CategoryName))
            {
                ModelState.AddModelError("Category", "Category already exists");
                return View(model);
            }

            var category = new Category
            {
                CategoryName = model.CategoryName,
            };

            _dbContext.Categories.Add(category);

            _dbContext.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            var category = _dbContext.Categories.FirstOrDefault(x => x.Id == id);
            if (category is null) return NotFound();

            _dbContext.Categories.Remove(category);
            _dbContext.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Update(int id)
        {
            var category = _dbContext.Categories.FirstOrDefault(x=>x.Id == id);
            if (category is null) return NotFound();

            var model = new CategoryUpdateVM
            {
                Id = category.Id,
                CategoryName = category.CategoryName
            };

            return View(model);
        }
        [HttpPost]
        public IActionResult Update(CategoryUpdateVM model)
        {
            if(model is null) return NotFound();

            var category = _dbContext.Categories.FirstOrDefault(x=> x.Id == model.Id);

            if (category is null) return NotFound();

            category.CategoryName = model.CategoryName;

            _dbContext.Categories.Update(category);

            _dbContext.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}

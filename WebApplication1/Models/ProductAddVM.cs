﻿using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using WebApplication1.Entities;

namespace WebApplication1.Models
{
    public class ProductAddVM
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [MinLength(2)]
        [MaxLength(55)]
        public string? Name { get; set; }
        [Required]
        [Range(0, double.PositiveInfinity)]
        public decimal Price { get; set; }
        public IFormFile? Photo { get; set; }
        [Required]
        public int CategoryId { get; set; }
        public List<SelectListItem>? Categories { get; set; }
    }
}

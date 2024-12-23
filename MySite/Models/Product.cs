﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MySite.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string FullDescription { get; set; }

        [Required]
        public decimal Price { get; set; }

        public int DiscountPrecent { get; set; }

        public decimal? DiscountPrice
        {
            get
            {
                if (DiscountPrecent > 0)
                {
                    return Price - (Price * DiscountPrecent / 100);
                }
                return null;
            }
        }

        [Required]
        public int Stock { get; set; }
        public DateTime CreatedDate { get; set; }

        public string? ImageUrl { get; set; }
        [Required]
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        [Required]
        public int BrandId { get; set; }
        public Brand Brand { get; set; } 
        [Required]
        public int AnimalTypeId { get; set; }
        public AnimalType AnimalType { get; set; }

        public string AgeCategory { get; set; }
        public string ProductSize { get; set; }
        public decimal ProductWeight { get; set; }
    }
}

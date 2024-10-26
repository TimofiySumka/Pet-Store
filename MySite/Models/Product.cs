using System.ComponentModel.DataAnnotations;

namespace MySite.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        public decimal Price { get; set; }

        public int DiscountPercentage { get; set; }
        public decimal? DiscountPrice
        {
            get
            {
                if (DiscountPercentage > 0)
                {
                    return Price - (Price * DiscountPercentage / 100);
                }
                return null;
            }
        }




        [DataType(DataType.Date)]
        public DateTime? ReleaseDate { get; set; }

        [Required]
        public int Stock { get; set; }
        public string? ImageUrl { get; set; }
        public ICollection<ProductCategory> ProductCategories { get; set; }

    }
}

using System.ComponentModel.DataAnnotations;

namespace MySite.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(255)]
        public string Description { get; set; }

        [Required]
        public decimal Price { get; set; }

        public decimal? DiscountPrice { get; set; }

        [StringLength(100)]
        public string Category { get; set; }

        [DataType(DataType.Date)]
        public DateTime? ReleaseDate { get; set; }

        [Required]
        public int Stock { get; set; }

        public string? ImageUrl { get; set; }
    }
}

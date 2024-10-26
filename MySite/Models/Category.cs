using System.ComponentModel.DataAnnotations;

namespace MySite.Models
{
    public class Category
    {
        public int CategoryId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

    }
}

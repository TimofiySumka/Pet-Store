using System.ComponentModel.DataAnnotations;

namespace MySite.Models
{
    public class AnimalType
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }


        public virtual ICollection<Product> Products { get; set; } = new List<Product>();
    }
}


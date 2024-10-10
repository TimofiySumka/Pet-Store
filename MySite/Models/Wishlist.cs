using System.ComponentModel.DataAnnotations;

namespace MySite.Models
{
    public class Wishlist
    {
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public int ProductId { get; set; }
    }
}

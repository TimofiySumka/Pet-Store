using MySite.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MySite.Models
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }

        [Required]
        public string UserId { get; set; } 
        public User User { get; set; } 

        [Required]
        public int CartId { get; set; } 
        public Cart Cart { get; set; } 

        [Required]
        public DateTime OrderDate { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalAmount { get; set; }

        [Required]
        [StringLength(500)]
        public string Address { get; set; }

        [Required]
        [StringLength(20)]
        public string Status { get; set; }

        public ICollection<OrderItem> OrderItems { get; set; }

    }
}

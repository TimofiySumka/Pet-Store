using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MySite.Models
{
    public class CartItem
    {
        [Key]
        public int CartItemId { get; set; }

        public int CartId { get; set; }

        public int ProductId { get; set; }

        public int Quantity { get; set; } = 1;

        public decimal Price { get; set; }

        [ForeignKey("CartId")]
        public virtual Cart Cart { get; set; } 

        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; } 
    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MySite.Models
{
    public class CartItem
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int CartId { get; set; }  // Внешний ключ для Cart
        public Cart Cart { get; set; }  // Навигационное свойство для Cart

        [Required]
        public int ProductId { get; set; }  // Внешний ключ для Product
        public Product Product { get; set; }  // Навигационное свойство для Product

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal UnitPrice { get; set; }  // Цена за единицу товара

        [Required]
        public int Quantity { get; set; }
    }
}

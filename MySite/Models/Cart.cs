using MySite.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MySite.Models
{
    public class Cart
    {
        [Key]
        public int CartId { get; set; }

        [Required]
        public string UserId { get; set; }  // Внешний ключ для User
        public User User { get; set; }  // Навигационное свойство для User

        public int? OrderId { get; set; }  // Внешний ключ для Order, может быть null, если нет заказа
        public Order Order { get; set; }  // Навигационное свойство для Order

        public ICollection<CartItem> CartItems { get; set; }
    }
}

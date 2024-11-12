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
        public string UserId { get; set; }  
        public User User { get; set; }  

        public int? OrderId { get; set; }  
        public Order Order { get; set; }  

        public ICollection<CartItem> CartItems { get; set; }
    }
}

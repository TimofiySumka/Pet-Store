﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MySite.Models
{
    public class CartItem
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int CartId { get; set; }  
        public Cart Cart { get; set; } 

        [Required]
        public int ProductId { get; set; }  
        public Product Product { get; set; } 

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal UnitPrice { get; set; } 

        [Required]
        public int Quantity { get; set; }
    }
}

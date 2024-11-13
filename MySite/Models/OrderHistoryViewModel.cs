using System.Collections.Generic;
using MySite.Models;

namespace MySite.Models.ViewModels
{
    public class OrderHistoryViewModel
    {
        public List<Order> Orders { get; set; }
        public List<CartItem> CartItems { get; set; } 
    }
}

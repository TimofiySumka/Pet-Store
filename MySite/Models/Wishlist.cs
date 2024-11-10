using Microsoft.AspNetCore.Identity;
using MySite.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MySite.Models
{
    public class Wishlist
    {
        public int Id { get; set; }

        [Required]
        [ForeignKey("User")]
        public string UserId { get; set; }

        [Required]
        public int ProductId { get; set; }


        [ForeignKey("UserId")]
        public virtual IdentityUser User { get; set; }

        public virtual Product Product { get; set; }
    }
}
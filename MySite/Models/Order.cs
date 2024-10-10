using System.ComponentModel.DataAnnotations;

namespace MySite.Models
{
    public class Order
    {
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime OrderDate { get; set; }

        [Required]
        public string Status { get; set; }

        [Required]
        public decimal TotalAmount { get; set; }

    }
}

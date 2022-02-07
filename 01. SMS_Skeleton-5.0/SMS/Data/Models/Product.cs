using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SMS.Data.Models
{
    public class Product
    {
        [Key]
        public string Id { get; init; } = Guid.NewGuid().ToString();

        [Required]
        [MaxLength(20)]
        public string Name { get; set; } // to be validated

        [Range(typeof(decimal), "0.05", "1000.00")]
        public decimal Price { get; set; } //to be validated

        [ForeignKey(nameof(Cart))]
        public string CartId { get; init; }

        public Cart Cart { get; set; }


    }
}

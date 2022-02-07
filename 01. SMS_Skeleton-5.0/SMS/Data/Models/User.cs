using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SMS.Data.Models
{
    public class User
    {
        [Key]
        public string Id { get; init; } = Guid.NewGuid().ToString();

        [Required]
        [MaxLength(20)]
        public string Username { get; set; } //validate max and min

        [Required]
        public string Email { get; set; } //validate email

        [Required]
        public string Password { get; set; } //to be hashed, validate min and max

        [ForeignKey(nameof(Cart))]
        public string CartId { get; init; }

        [Required]
        public Cart Cart { get; set; }
    }
}

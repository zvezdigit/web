using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static FootballManager.Data.DataConstants;

namespace FootballManager.Data.Models
{
    public class User
    {
        //        •	Has an Id – a string, Primary Key
        //•	Has a Username – a string with min length 5 and max length 20 (required)
        //•	Has an Email – a string with min length 10 and max length 60 (required)
        //•	Has a Password – a string with min length 5 and max length 20 (before hashed)  - no max length required for a hashed password in the database(required)
        //•	Has UserPlayers collection

        public User()
        {
            this.UserPlayers = new HashSet<UserPlayer>();
        }

        [Key]
        [MaxLength(36)]
        public string Id { get; init; } = Guid.NewGuid().ToString();

        [Required]
        [MaxLength(DefaulMaxtLength)]
        public string Username { get; set; }

        [Required]
        [MaxLength(EmailMaxLength)]
        public string Email { get; set; }

        [Required]
        [MaxLength(64)]
        public string Password { get; set; }

        public ICollection<UserPlayer> UserPlayers { get; set; } 

    }
}

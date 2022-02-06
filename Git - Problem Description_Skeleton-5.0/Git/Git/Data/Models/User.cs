using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Git.Data.Models
{
    public class User
    {
        // •Has an Id – a string, Primary Key
        //•	Has a Username – a string with min length 5 and max length 20 (required)
        //•	Has an Email - a string (required)
        //•	Has a Password – a string with min length 6 and max length 20  - hashed in the database(required)
        //•	Has Repositories collection – a Repository type
        //•	Has Commits collection – a Commit type

        public User()
        {
            this.Repositories = new List<Repository>();
            this.Commits = new List<Commit>();
        }
        [Key]
        public string Id { get; init; } = Guid.NewGuid().ToString();

        [Required]
        [MaxLength(20)]
        public string Username { get; set; } //to be validated

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; } //to be validated and hashed

        public ICollection<Repository> Repositories { get; set; }

        public ICollection<Commit> Commits { get; set; }
    }
}

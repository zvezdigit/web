using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Git.Data.Models
{
    public class Repository
    {
        //        •	Has an Id – a string, Primary Key
        //•	Has a Name – a string with min length 3 and max length 10 (required)
        //•	Has a CreatedOn – a datetime(required)
        //•	Has a IsPublic – bool (required)
        //•	Has a OwnerId – a string
        //•	Has a Owner – a User object
        //•	Has Commits collection – a Commit type


        public Repository()
        {
            this.Commits = new HashSet<Commit>();
        }
        [Key]
        public string Id { get; init; } = Guid.NewGuid().ToString();

        [Required]
        [MaxLength(10)]
        public string Name { get; set; } //to be validated

        [Required]
        public DateTime CreatedOn { get; set; }

        public bool IsPublic { get; set; }

        [ForeignKey(nameof(Owner))]
        [Required]
        public string OwnerId { get; set; }

        public User Owner { get; set; }

        public ICollection<Commit> Commits { get; set; }

    }
}

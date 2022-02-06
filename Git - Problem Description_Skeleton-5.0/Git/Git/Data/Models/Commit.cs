using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Git.Data.Models
{
    public class Commit
    {
        //        •	Has an Id – a string, Primary Key
        //•	Has a Description – a string with min length 5 (required)
        //•	Has a CreatedOn – a datetime(required)
        //•	Has a CreatorId – a string
        //•	Has Creator – a User object
        //•	Has RepositoryId – a string
        //•	Has Repository– a Repository object

        [Key]
        public string Id { get; init; } = Guid.NewGuid().ToString();

        [Required]
        public string Description { get; set; } //to be validated

        [Required]
        public DateTime CreatedOn { get; set; }

        [ForeignKey(nameof(Creator))]
        [Required]
        public string CreatorId { get; set; }

        public User Creator { get; set; }

        [ForeignKey(nameof(Repository))]
        [Required]
        public string RepositoryId { get; set; }

        public Repository Repository { get; set; }
    }
}

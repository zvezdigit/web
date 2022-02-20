using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballManager.Data.Models
{
    public class UserPlayer
    {
        //        •	Has UserId – a string
        //•	Has User – a User object
        //•	Has PlayerId – an int
        //•	Has Player – a Player object

        [ForeignKey(nameof(User))]
        public string UserId { get; set; }

        public User User { get; set; }

        [ForeignKey(nameof(Player))]
        public string PlayerId { get; set; }

        public Player Player { get; set; }

    }
}

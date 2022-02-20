using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballManager.ViewModels.Players
{
    public class ListAllPlayersViewModel
    {
        public ICollection<PlayerViewModel> Players { get; set; }
    }
}

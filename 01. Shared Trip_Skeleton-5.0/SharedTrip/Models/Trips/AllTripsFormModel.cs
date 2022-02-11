using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedTrip.Models.Trips
{
    public class AllTripsFormModel
    {
        public ICollection<TripFormModel> Trips { get; set; }
    }
}

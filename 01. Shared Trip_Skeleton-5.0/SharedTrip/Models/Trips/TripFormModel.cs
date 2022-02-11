using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedTrip.Models.Trips
{
    public class TripFormModel
    {
        public string Id { get; init; }
        public string StartPoint { get; set; }

        public string EndPoint { get; set; }

        public DateTime DepartureTime { get; set; }

        public string DepartureTimeString => DepartureTime.ToString("dd.MM.yyyy HH:mm", CultureInfo.CreateSpecificCulture("bg-BG"));

        public int Seats { get; set; }

        public string Description { get; set; }

    }
}

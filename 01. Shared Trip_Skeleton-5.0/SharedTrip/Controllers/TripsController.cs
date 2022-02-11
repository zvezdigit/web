using Microsoft.EntityFrameworkCore;
using MyWebServer.Controllers;
using MyWebServer.Http;
using SharedTrip.Data;
using SharedTrip.Data.Models;
using SharedTrip.Models;
using SharedTrip.Models.Trips;
using SharedTrip.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedTrip.Controllers
{
    public class TripsController: Controller
    {
        private readonly IValidator validator;
        private readonly ApplicationDbContext data;

        public TripsController(IValidator validator, ApplicationDbContext data)
        {
            this.validator = validator;
            this.data = data;
        }

        [Authorize]
        public HttpResponse Add()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public HttpResponse Add(AddTripFormModel model)
        {
            var modelErrors = this.validator.ValidateTrip(model);

            if (modelErrors.Any())
            {
                return Error(modelErrors);
            }

            var trip = new Trip
            {

                StartPoint = model.StartPoint,
                EndPoint = model.EndPoint,
                DepartureTime = model.DepartureTime,
                ImagePath = model.ImagePath,
                Seats = model.Seats,
                Description = model.Description,
                UserTrips = new List<Data.Models.UserTrip>()
            };

            data.Trips.Add(trip);
            data.SaveChanges();

            return Redirect("/Trips/All");
        }

        [Authorize]
        public HttpResponse All()
        {

            var trips = data.Trips
                .Select(x => new
                {
                    Id =x.Id,
                    StartPoint = x.StartPoint,
                    EndPoint = x.EndPoint,
                    DepartureTime = x.DepartureTime,
                    Seats = x.Seats
                })
                .ToList();

            var viewModel = new AllTripsFormModel
            {
                Trips = trips.Select(x => new TripFormModel
                {
                    Id= x.Id,
                    StartPoint = x.StartPoint,
                    EndPoint= x.EndPoint,
                    DepartureTime= x.DepartureTime,
                    Seats= x.Seats
                })
                .ToList()
            };

            return View(viewModel);
        }

        [Authorize]
        public HttpResponse Details(string tripId)
        {
            var tripID = data.Trips
                .Where(t=>t.Id==tripId)
                .Select(t=>t.Id)
                .FirstOrDefault();

            var trip = data.Trips
                .First(t => t.Id == tripID);

            var viewModel = new TripFormModel
            {
                Id = trip.Id,
                StartPoint = trip.StartPoint,
                EndPoint = trip.EndPoint,
                DepartureTime = trip.DepartureTime,
                Seats = trip.Seats,
                Description = trip.Description
            };

            return View(viewModel);
        }

        [Authorize]
        public HttpResponse AddUserToTrip(string tripId)
        {
            var tripID = data.Trips
                .Where(t => t.Id == tripId)
                .Select(t => t.Id)
                .FirstOrDefault();

            var trip = data.Trips
                .First(t => t.Id == tripID);

            var user = data.Users.First(x => x.Id == User.Id);

            var userTrip = new Data.Models.UserTrip
            {
                TripId = tripID,
                Trip = trip,
                UserId = user.Id,
                User = user
            };

            var newUserID = data.UserTrips
                .Include(x => x.Trip)
                .Where(x =>  x.UserId == user.Id && x.TripId==tripID)
                .Select(x => x.UserId)
                .FirstOrDefault();



            if (newUserID!=null)
            {
               //  return Error($" You can not join the trip because it has already joined to the trip.");

                return Redirect($"/Trips/Details?tripId={tripID} ");
            }

            trip.UserTrips.Add(userTrip);

            if (trip.Seats==0)
            {
                return Error($"Seats are 0. There is no more place for this trip. Join another trip.");
            }
            trip.Seats -= 1;

            
            data.SaveChanges();

            return Redirect("/Home");
        }
    }
}

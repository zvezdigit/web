using FootballManager.Contracts;
using FootballManager.Data;
using FootballManager.Data.Models;
using FootballManager.ViewModels.Players;
using Microsoft.EntityFrameworkCore;
using MyWebServer.Controllers;
using MyWebServer.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballManager.Controllers
{
    public class PlayersController: Controller
    {
        private readonly IValidator validator;
        private readonly FootballManagerDbContext data;

        public PlayersController(IValidator _validator, FootballManagerDbContext data)
        {
            this.validator = _validator;
            this.data = data;
        }


        [Authorize]
        public HttpResponse All()
        {
            var players = data.Players.Select(p => new
            {
                Id=p.Id,
                ImageUrl = p.ImageUrl,
                FullName = p.FullName,
                Position = p.Position,
                Description = p.Description,
                Speed = p.Speed,
                Endurance = p.Endurance
            }).ToList();



            var model = new ListAllPlayersViewModel
            {
                Players = players.Select(p => new PlayerViewModel
                {
                    Id = p.Id,
                    ImageUrl = p.ImageUrl,
                    FullName = p.FullName,
                    Position = p.Position,
                    Description = p.Description,
                    Speed = p.Speed,
                    Endurance = p.Endurance

                }).ToList()
            };
            return View(model);
        }

        [Authorize]
        public HttpResponse Add()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public HttpResponse Add(AddPlayerFormModel model)
        {
            var modelErrors = this.validator.ValidatePlayer(model);


            if (modelErrors.Any())
            {
                //return Error(modelErrors);
                return Redirect("/Players/Add");
            }

            var player = new Player
            {
                ImageUrl = model.ImageUrl,
                Description = model.Description,
                FullName = model.FullName,
                Position=model.Position,
                Endurance=model.Endurance,
                Speed = model.Speed
            };

            try
            {
                data.Players.Add(player);

                data.SaveChanges();
            }
            catch (Exception)
            {

                //return Error("Player could not be saved in DB.");
                return Redirect("/Players/Add");
            }

            return Redirect("/Players/All");
        }

        [Authorize]
        [HttpGet]
        public HttpResponse AddToCollection(string playerId)
        {
            var player = data.Players
                .FirstOrDefault(p => p.Id == playerId);

            var user = data.Users
                .FirstOrDefault(u => u.Id == User.Id);

            var userPlayer = new UserPlayer
            {
                User = user,
                UserId = user.Id,
                Player = player,
                PlayerId = playerId
            };

            var userPlayerNew = data.UserPlayers
                 .Where(x => x.UserId == user.Id && x.PlayerId == playerId)
                 .FirstOrDefault();

            if (userPlayerNew!=null)
            {
                return Redirect($"/Players/All");
            }

            data.UserPlayers.Add(userPlayer);
            data.SaveChanges();

            return Redirect("/Players/All");
        }

        [Authorize]
        public HttpResponse Collection()
        {
          

            var userPlayersCollection = data.Players
                .Include(x => x.UserPlayers)
                .ThenInclude(x => x.User)
                .Where(x => x.UserPlayers.Any(up=>up.UserId==User.Id))
                .Select(p => new
                {
                    Id = p.Id,
                    ImageUrl = p.ImageUrl,
                    FullName = p.FullName,
                    Position = p.Position,
                    Description = p.Description,
                    Speed = p.Speed,
                    Endurance = p.Endurance
                })
                .ToList();

            var model = new ListAllPlayersViewModel
            {
                Players = userPlayersCollection.Select(p => new PlayerViewModel
                {
                    Id = p.Id,
                    ImageUrl = p.ImageUrl,
                    FullName = p.FullName,
                    Position = p.Position,
                    Description = p.Description,
                    Speed = p.Speed,
                    Endurance = p.Endurance

                }).ToList()
            };

            return View(model);
        }

        [Authorize]
        [HttpGet]
        public HttpResponse RemoveFromCollection(string playerId)
        {
            var player = data.Players
                .FirstOrDefault(p => p.Id == playerId);

            var userPlayer = data.UserPlayers
                .FirstOrDefault(up => up.UserId == User.Id && up.PlayerId == player.Id);

            if (userPlayer== null)
            {
                return Redirect($"/Players/RemoveFromCollection?playerId={playerId}");
            }

            data.UserPlayers.Remove(userPlayer);
            data.SaveChanges();

            return Redirect("/Players/Collection");
        }
    }
}

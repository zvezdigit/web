using FootballManager.Contracts;
using FootballManager.ViewModels.Players;
using FootballManager.ViewModels.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static FootballManager.Data.DataConstants;

namespace FootballManager.Services
{
    public class Validator : IValidator
    {
        public ICollection<string> ValidatePlayer(AddPlayerFormModel player)
        {
            var errors = new List<string>();

            if (player.FullName == null || player.FullName.Length < DefaultMinLength || player.FullName.Length > FullNameMaxLength)
            {
                errors.Add($"Fullname '{player.FullName}' of the player is not valid. It must be between {DefaultMinLength} and {FullNameMaxLength} characters long.");
            }

            if (player.Position == null || player.Position.Length < DefaultMinLength || player.Position.Length > DefaulMaxtLength)
            {
                errors.Add($"Position '{player.Position}' of the player is not valid. It must be between {DefaultMinLength} and {DefaulMaxtLength} characters long.");
            }

            if (string.IsNullOrEmpty(player.Description) || player.Description.Length > DescriptionMaxLength)
            {
                errors.Add($"Description '{player.Description}' of the player is not valid. It must be with maximum {DescriptionMaxLength} characters long.");
            }

            if (string.IsNullOrEmpty(player.ImageUrl))
            {
                errors.Add($"UrlImage  of the player should be added.");
            }

            if ( player.Speed<MinValue || player.Speed>MaxValue)
            {
                errors.Add($"Speed '{player.Speed}' of the player is not valid. It must be between {MinValue} and {MaxValue}.");
            }

            if (player.Endurance < MinValue || player.Endurance > MaxValue)
            {
                errors.Add($"Endurance '{player.Endurance}' of the player is not valid. It must be between {MinValue} and {MaxValue}.");
            }

            return errors;
        }

        public ICollection<string> ValidateUser(RegisterUserFormModel user)
        {
            var errors = new List<string>();

            if (user.Username == null || user.Username.Length < DefaultMinLength || user.Username.Length > DefaulMaxtLength)
            {
                errors.Add($"Username '{user.Username}' is not valid. It must be between {DefaultMinLength} and {DefaulMaxtLength} characters long.");
            }

            if (user.Email == null || !Regex.IsMatch(user.Email, UserEmailRegularExpression))
            {
                errors.Add($"Email '{user.Email}' is not a valid e-mail address.");
            }

            if (user.Email.Length<EmailMinLength || user.Email.Length>EmailMaxLength)
            {
                errors.Add($"Email '{user.Email}' is not valid. It must be between {EmailMinLength} and {EmailMaxLength} characters long.");
            }

            if (user.Password == null || user.Password.Length < DefaultMinLength || user.Password.Length > DefaulMaxtLength)
            {
                errors.Add($"The provided password is not valid. It must be between {DefaultMinLength} and {DefaulMaxtLength} characters long.");
            }

            if (user.Password != null && user.Password.Any(x => x == ' '))
            {
                errors.Add($"The provided password cannot contain whitespaces.");
            }

            if (user.Password != user.ConfirmPassword)
            {
                errors.Add("Password and its confirmation are different.");
            }

            return errors;
        }
    }
}

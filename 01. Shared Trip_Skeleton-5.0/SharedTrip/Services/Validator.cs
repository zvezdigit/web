using SharedTrip.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static SharedTrip.Data.DataConstants;

namespace SharedTrip.Services
{
    public class Validator : IValidator
    {
        public ICollection<string> ValidateTrip(AddTripFormModel trip)
        {
            var errors = new List<string>();

            if (trip.StartPoint==null || trip.EndPoint==null)
            {
                errors.Add("Please add Startpoint/EndPoint.");
            }

            if (trip.Seats<2 || trip.Seats>6)
            {
                errors.Add($"The value of the seats '{trip.Seats}' is not valid.It should be between {SeatsMinValue} and {SeatsMaxValue} included.");
            }

            if (trip.Description == null || trip.Description.Length > DescriptionMaxLength)
            {
                errors.Add($"A description of the trip '{trip.Description}' is not valid. It must be with maximun length long{DescriptionMaxLength}.");
            }


            return errors;
        }

        public ICollection<string> ValidateUser(RegisterUserFormModel user)
        {
            var errors = new List<string>();

            if (user.Username == null || user.Username.Length < UsernameMinLength || user.Username.Length > DefaultMaxLength)
            {
                errors.Add($"Username '{user.Username}' is not valid. It must be between {UsernameMinLength} and {DefaultMaxLength} characters long.");
            }

            if (user.Email == null || !Regex.IsMatch(user.Email, UserEmailRegularExpression))
            {
                errors.Add($"Email '{user.Email}' is not a valid e-mail address.");
            }

            if (user.Password == null || user.Password.Length < PasswordMinLength || user.Password.Length > DefaultMaxLength)
            {
                errors.Add($"The provided password is not valid. It must be between {PasswordMinLength} and {DefaultMaxLength} characters long.");
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

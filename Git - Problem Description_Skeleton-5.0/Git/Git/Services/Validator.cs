using Git.ViewModel.Commits;
using Git.ViewModel.Repositories;
using Git.ViewModel.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static Git.Data.DataConstants;

namespace Git.Services
{
    public class Validator : IValidator
    {
        public ICollection<string> ValidateCommit(CreateCommitFormModel commit)
        {
            var errors = new List<string>();

            if (commit.Description == null || commit.Description.Length < DefaultMinLength)
            {
                errors.Add($"A description of the commit '{commit.Description}' is not valid. It must be with minimum length long{DefaultMinLength}.");
            }


            return errors;
        }

        public ICollection<string> ValidateRepository(CreateRepositoryFormModel repository)
        {
            var errors = new List<string>();

            if (repository.Name==null || repository.Name.Length<RepositoryNameMinLength || repository.Name.Length>RepositoryNameMaxLength)
            {
                errors.Add($"A name of the repository '{repository.Name}' is not valid. It must be between {RepositoryNameMinLength} and {RepositoryNameMaxLength} characters long.");
            }


            return errors;
        }

            public ICollection<string> ValidateUser(RegisterUserModelForm user)
        {
            var errors = new List<string>();

            if (user.Username == null || user.Username.Length < DefaultMinLength || user.Username.Length > DefaultMaxLength)
            {
                errors.Add($"Username '{user.Username}' is not valid. It must be between {DefaultMinLength} and {DefaultMaxLength} characters long.");
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

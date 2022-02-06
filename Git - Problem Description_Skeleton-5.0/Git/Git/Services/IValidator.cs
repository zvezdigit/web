using Git.ViewModel.Commits;
using Git.ViewModel.Repositories;
using Git.ViewModel.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Git.Services
{
    public interface IValidator
    {
        ICollection<string> ValidateUser(RegisterUserModelForm user);

        ICollection<string> ValidateCommit(CreateCommitFormModel commit);

        ICollection<string> ValidateRepository(CreateRepositoryFormModel repository);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Git.ViewModel.Repositories
{
    public class AllRepositoriesFormModel
    {
        public ICollection<RepositoryFormModel> Repositories { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Git.ViewModel.Repositories
{
    public class RepositoryFormModel
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public string Owner { get; set; }

        public DateTime CreatedOn { get; set; }

        public int CommitsCount { get; set; }
    }
}

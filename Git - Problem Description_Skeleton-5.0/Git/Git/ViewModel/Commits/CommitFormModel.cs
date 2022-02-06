using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Git.ViewModel.Commits
{
    public class CommitFormModel
    {
        public string Id { get; set; }
        public string Repository { get; set; }
        public string Description { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}

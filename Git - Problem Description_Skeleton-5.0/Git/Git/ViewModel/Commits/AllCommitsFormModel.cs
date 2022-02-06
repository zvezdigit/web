using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Git.ViewModel.Commits
{
    public class AllCommitsFormModel
    {
        public ICollection<CommitFormModel> Commits { get; set; }
    }
}

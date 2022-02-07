using SMS.Models.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Models.Users
{
    public class IndexLoggedInModel
    {
        public string Username { get; set; }

        public IEnumerable<ProductViewModel> Products { get; set; }
    }
}

using SMS.Models.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Models
{
    public class AddProductsFormModel
    {
        public ICollection<CreateProductFormModel> Products { get; set; }
    }
}

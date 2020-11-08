using System;
using System.Collections.Generic;
using System.Text;

namespace eKitchen.Models
{
    public interface IMeal
    {
        List<Product> Products { get; set; }
    }
}

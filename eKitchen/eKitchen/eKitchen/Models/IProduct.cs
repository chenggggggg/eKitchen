using System;
using System.Collections.Generic;
using System.Text;

namespace eKitchen.Models
{
    public interface IProduct
    {
        int ID { get; set; }
        string Name { get; set; }
        string Category { get; set; }
        List<Nutrient> Nutrients { get; set; }
    }
}

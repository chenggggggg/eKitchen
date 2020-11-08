using System;
using System.Collections.Generic;
using System.Text;

namespace eKitchen.Models
{
    public class Product : IProduct
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public List<Nutrient> Nutrients { get; set; }
    }
}

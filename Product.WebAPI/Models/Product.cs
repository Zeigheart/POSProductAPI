using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Product.WebAPI.Models
{
    public class Product
    {
        public string categoryId { get; set; }

        public string productId { get; set; }

        public string name { get; set; }

        public decimal? price { get; set; }
        public int? units { get; set; }
        public string picutureUrl { get; set; }

    }
}

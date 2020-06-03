using System;
using System.Collections.Generic;
using System.Text;

namespace Model.DTOs
{
    public class ProductCreateDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }
}

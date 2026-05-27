using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DEMO1.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        public string Article { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public int Price {  get; set; }
        public string Category { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public string? ImagePath { get; set; }
        public int Discount { get; set; }  
    }
}

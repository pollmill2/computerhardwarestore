using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System;

namespace ComputerHardwareStore.Entities
{
    [Index(nameof(ProductName), IsUnique = true)]
    public class Product : BaseEntity
    {
        public string ProductName { get; set; }
        public int? CategoryId { get; set; }
        public virtual Category Category { get; set; }
        public string Image { get; set; }
        public double Price { get; set; }
        public double Rating { get; set; }
        public DateTime Date { get; set; }
        public List<Review> Reviews { get; set; }
        public string Specification { get; set; }
    }
}

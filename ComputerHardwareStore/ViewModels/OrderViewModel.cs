using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ComputerHardwareStore.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace ComputerHardwareStore.ViewModels
{
    public class OrderViewModel
    {
        [Required]
        public string Address { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        public string FName { get; set; }
        [Required]
        public string LName { get; set; }
        public ShoppingCart ShoppingCart{ get; set; }
    }
}

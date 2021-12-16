using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using ComputerHardwareStore.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace ComputerHardwareStore.ViewModels
{
    public class AddProductViewModel
    {
        [Required]
        public string ProductName { get; set; }
        public string Image { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public string Specification { get; set; }
        [Required]
        public int? CategoryId { get; set; }
    }
}

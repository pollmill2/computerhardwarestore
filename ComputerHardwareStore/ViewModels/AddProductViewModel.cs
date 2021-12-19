using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using ComputerHardwareStore.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace ComputerHardwareStore.ViewModels
{
    public class AddProductViewModel
    {
        [Required(ErrorMessage = "Поле не может быть пустым")]
        [StringLength(40, ErrorMessage = "Поле {0} должно иметь минимум {2} и максимум {1} символов.", MinimumLength = 2)]
        public string ProductName { get; set; }
        public string Image { get; set; }
        [Required(ErrorMessage = "Поле не может быть пустым")]
        public double Price { get; set; }
        [Required(ErrorMessage = "Поле не может быть пустым")]
        public DateTime Date { get; set; }
        [Required(ErrorMessage = "Поле не может быть пустым")]
        [StringLength(300, ErrorMessage = "Поле {0} должно иметь минимум {2} и максимум {1} символов.", MinimumLength = 7)]
        public string Specification { get; set; }
        [Required]
        public int? CategoryId { get; set; }
    }
}

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
        [Required(ErrorMessage = "Поле не может быть пустым")]
        [StringLength(60, ErrorMessage = "Поле {0} должно иметь минимум {2} и максимум {1} символов.", MinimumLength = 10)]
        public string Address { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required(ErrorMessage = "Поле не может быть пустым")]
        [StringLength(50, ErrorMessage = "Поле {0} должно иметь минимум {2} и максимум {1} символов.", MinimumLength = 3)]
        public string FName { get; set; }
        [Required(ErrorMessage = "Поле не может быть пустым")]
        [StringLength(50, ErrorMessage = "Поле {0} должно иметь минимум {2} и максимум {1} символов.", MinimumLength = 3)]
        public string LName { get; set; }
        public ShoppingCartViewModel ShoppingCart { get; set; }
    }
}

using ComputerHardwareStore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComputerHardwareStore.ViewModels
{
    public class ShoppingCartViewModel
    {
        public List<ShoppingCartItemViewModel> ShoppingCart { get; set; }
        public double ShoppingCartTotal => ShoppingCart.Sum(x => x.Product.Price);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComputerHardwareStore.Entities
{
    public class Order : BaseEntity
    {
        public string Address { get; set; }
        public string Phone { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
        public List<ShoppingCartItem> CardItems { get; set; }
    }
}

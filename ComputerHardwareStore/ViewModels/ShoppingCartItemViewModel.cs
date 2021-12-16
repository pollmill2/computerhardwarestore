using ComputerHardwareStore.Entities;

namespace ComputerHardwareStore.ViewModels
{
    public class ShoppingCartItemViewModel : BaseEntity
    {
        public int ShoppingCardItemId { get; set; }
        public Product Product { get; set; }
        public string ShoppingCartId { get; set; }
    }
}

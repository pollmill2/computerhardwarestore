namespace ComputerHardwareStore.Entities
{
    public class ShoppingCartItem : BaseEntity
    {
        public Product Product { get; set; }
        public string ShoppingCartId { get; set; }
    }
}

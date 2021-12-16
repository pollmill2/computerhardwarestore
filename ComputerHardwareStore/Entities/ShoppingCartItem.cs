namespace ComputerHardwareStore.Entities
{
    public class ShoppingCartItem : BaseEntity
    {
        public int ShoppingCardOtemIdd { get; set; }
        public Product Product { get; set; }
        public string ShoppingCartId { get; set; }
    }
}

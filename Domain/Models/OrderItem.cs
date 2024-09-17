namespace Domain.Entities
{
    public class OrderItem
    {
        public int OrderId { get; set; }
        public Order Order { get; set; }
            = default!;
        public int ProductId { get; set; }
        public Product Product { get; set; }
            = default!;
        public int Quantity { get; set; }
    }
}
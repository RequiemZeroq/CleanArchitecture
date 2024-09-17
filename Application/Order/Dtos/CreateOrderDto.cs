namespace UseCases.Order.Dtos
{
    public class CreateOrderDto
    {
        public List<OrderItemDto> Item { get; set; }
    }
}
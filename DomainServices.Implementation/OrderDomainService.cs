using Domain.Entities;
using DomainServices.Interfaces;

namespace DomainServices.Implementation
{
    public class OrderDomainService : IOrderDomainService
    {
        public decimal GetTotal(
            Order order,
            CalculateDeliveryCost deliveryCostCalculator)
        {
            decimal totalPrice = order.Items.Sum(x => x.Quantity * x.Product.Price);
            decimal deliveryCost = 0M;
            if(totalPrice < 1000)
            {
                var totalWeight = order.Items.Sum(x => x.Product.Weigth);
                deliveryCost = deliveryCostCalculator.Invoke(totalWeight);
            }

            return totalPrice + deliveryCost;
        }
    }
}

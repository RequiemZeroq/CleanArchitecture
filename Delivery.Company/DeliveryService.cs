﻿using Delivery.Interfaces;

namespace Delivery.Company
{
    public class DeliveryService : IDeliveryService
    {
        public decimal CalculateDeliveryCost(float weight)
        {
            return (decimal)weight * 10;
        }
        public Task<bool> IsDeliveredAsync(int orderId)
        {
            return Task.FromResult(true);
        }
    }
}

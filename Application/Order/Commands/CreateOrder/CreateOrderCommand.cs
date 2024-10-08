﻿using MediatR;
using UseCases.Order.Dtos;

namespace UseCases.Order.Commands.CreateOrder
{
    public class CreateOrderCommand : IRequest<int>
    {
        public CreateOrderDto Dto { get; set; }
    }
}

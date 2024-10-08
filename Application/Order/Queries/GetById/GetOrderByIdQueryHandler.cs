﻿using AutoMapper;
using DataAccess.Interfaces;
using Delivery.Interfaces;
using DomainServices.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;
using UseCases.Exeptions;
using UseCases.Order.Dtos;

namespace UseCases.Order.Queries.GetById
{
    public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQuery, OrderDto>
    {
        private readonly IMapper _mapper;
        private readonly IDbContext _dbContext;
        private readonly IOrderDomainService _orderDomainService;
        private readonly IDeliveryService _deliveryService;

        public GetOrderByIdQueryHandler(
            IMapper mapper,
            IDbContext dbContext,
            IOrderDomainService orderDomainService,
            IDeliveryService deliveryService)
        {
            _mapper = mapper;
            _dbContext = dbContext;
            _orderDomainService = orderDomainService;
            _deliveryService = deliveryService;
        }

        public async Task<OrderDto> Handle(GetOrderByIdQuery query, CancellationToken cancellationToken)
        {
            var order = await _dbContext.Orders
                .AsNoTracking()
                .Include(x => x.Items)
                    .ThenInclude(x => x.Product)
                .FirstOrDefaultAsync(x => x.Id == query.Id);

            if (order is null)
                throw new EntityNotFoundExeption();

            var dto = _mapper.Map<OrderDto>(order);
            dto.Total = _orderDomainService.GetTotal(order, _deliveryService.CalculateDeliveryCost);

            return dto;
        }
    }
}

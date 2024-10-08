﻿using AutoMapper;
using DataAccess.Interfaces;
using Email.Interfaces;
using MediatR;
using WebApp.Interfaces;

namespace UseCases.Order.Commands.CreateOrder
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, int>
    {
        private readonly IMapper _mapper;
        private readonly IDbContext _dbContext;
        private readonly IBackgroundJobService _backgroundJobService;
        private readonly ICurrentUserService _currentUserService;

        public CreateOrderCommandHandler(
            IMapper mapper,
            IDbContext dbContext,
            IBackgroundJobService backgroundJobService,
            ICurrentUserService currentUserService)
        {
            _mapper = mapper;
            _dbContext = dbContext;
            _backgroundJobService = backgroundJobService;   
            _currentUserService = currentUserService;
        }

        public async Task<int> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
        {
            var order = _mapper.Map<Domain.Entities.Order>(command.Dto);

            _dbContext.Orders.Add(order);
            await _dbContext.SaveChangesAsync();


            string currentUserEmail = _currentUserService.Email;
            _backgroundJobService.Schedule<IEmailService>(emailService => emailService.SendAsync(
                currentUserEmail,
                "New order",
                $"Order {order.Id} created sucessfully"));

            return order.Id;
        }
    }
}

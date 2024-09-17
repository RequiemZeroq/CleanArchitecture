using UseCases;
using Azure.Core.Pipeline;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UseCases.Order.Dtos;
using MediatR;
using UseCases.Order.Queries.GetById;
using UseCases.Order.Commands.CreateOrder;

namespace WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly ISender _sender;

        public OrdersController(ISender sender)
        {
            _sender = sender;
        }

        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<OrderDto> Get(int id)
        {
            var result = await _sender.Send(new GetOrderByIdQuery { Id = id }); 
            return result;
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<int> Create([FromBody] CreateOrderDto dto)
        {
            var id = await _sender.Send(new CreateOrderCommand { Dto = dto }); 
            return id;
        }
    }
}

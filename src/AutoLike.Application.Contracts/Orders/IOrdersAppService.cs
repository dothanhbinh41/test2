using AutoLike.Orders.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace AutoLike.Orders
{
    public interface IOrdersAppService : IApplicationService
    {
        Task<OrderDto> CreateAsync(CreateOrderDto order);
        Task<OrderDto> CancelAsync(Guid id);
    }
}
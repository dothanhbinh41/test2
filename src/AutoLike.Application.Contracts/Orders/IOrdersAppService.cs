using AutoLike.Orders.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace AutoLike.Orders
{
    public interface IOrdersAppService : IApplicationService
    {
        Task<OrderDto> CreateAsync(CreateOrderDto order);
        Task<OrderDto> CancelAsync(Guid id);
        Task<OrderDto> ProcessOrderAsync(CreateOrderProcessDto request); 
        Task<PagedResultDto<OrderDto>> GetOrdersAsync(string serviceCode,
            OrderStatus? status,
            DateTime? from,
            DateTime? to,
            int skip = 0,
            int max = 10);
    }
}
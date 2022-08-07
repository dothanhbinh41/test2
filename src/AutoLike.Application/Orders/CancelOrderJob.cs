using AutoLike.Transactions;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Repositories.MongoDB;

namespace AutoLike.Orders
{
    public class CancelOrderJob : AsyncBackgroundJob<CancelOrderArgs>, ITransientDependency
    { 
        private readonly ITransactionService transactionService;
        private readonly IRepository<Order, Guid> orderRepository;
        private readonly IRepository<OrderProcess, Guid> orderProcessRepository;

        public CancelOrderJob( 
            ITransactionService transactionService,
            IRepository<Order, Guid> orderRepository,
            IRepository<OrderProcess, Guid> orderProcessRepository)
        { 
            this.transactionService = transactionService;
            this.orderRepository = orderRepository;
            this.orderProcessRepository = orderProcessRepository;
        }
        public override async Task ExecuteAsync(CancelOrderArgs args)
        {
            var order = await orderRepository.GetAsync(args.OrderId);
            if (order == null)
            {
                throw new UserFriendlyException("Order notfound");
            }
            var completeValue = await CalculateCompleteValue(args.OrderId);
            var refund = order.Price - completeValue * (order.Speed.Price + order.Warranty.Price);
            if (refund <= 0)
            {
                return;
            }
             
            await transactionService.TranferToUserAsync(order.User, refund, new TransactionInformation { Id = order.Id, Code = order.Code }, TransactionType.Refund);
            order.Status = OrderStatus.InActive;
            await orderRepository.UpdateAsync(order);  
        }

        private Task<int> CalculateCompleteValue(Guid orderId)
        {
            return Task.FromResult(0);
        }
    }
}

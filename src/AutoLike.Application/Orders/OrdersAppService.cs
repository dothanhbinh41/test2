using AutoLike.Generators;
using AutoLike.Orders.Dtos;
using AutoLike.Services;
using AutoLike.Transactions;
using AutoLike.Users;
using AutoLike.Validators;
using IdentityServer4.Models;
using Microsoft.AspNetCore.Authorization;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Guids;
using Volo.Abp.Identity;
using Volo.Abp.ObjectMapping;

namespace AutoLike.Orders
{
    [Authorize]
    public class OrdersAppService : AutoLikeAppService, IOrdersAppService
    {
        public const double CancelTime = 15;//mins
        private readonly IGuidGenerator guidGenerator;
        private readonly IUidValidator uidValidator;
        private readonly ICodeGenerator codeGenerator;
        private readonly IBackgroundJobManager backgroundJobManager;
        private readonly IMongoClient mongoClient;
        private readonly ITransactionService transactionService;
        private readonly IRepository<OrderProcess, Guid> orderProcessRepository;
        private readonly IRepository<Order, Guid> orderRepository;
        private readonly IRepository<Service, Guid> serviceRepository;

        public OrdersAppService(
            IGuidGenerator guidGenerator,
            IUidValidator uidValidator,
            ICodeGenerator codeGenerator,
            IBackgroundJobManager backgroundJobManager,
            ITransactionService transactionService,
            IRepository<OrderProcess, Guid> orderProcessRepository,
            IRepository<Order, Guid> orderRepository,
            IRepository<Service, Guid> serviceRepository)
        {
            this.guidGenerator = guidGenerator;
            this.uidValidator = uidValidator;
            this.codeGenerator = codeGenerator;
            this.backgroundJobManager = backgroundJobManager;
            this.transactionService = transactionService;
            this.orderProcessRepository = orderProcessRepository;
            this.orderRepository = orderRepository;
            this.serviceRepository = serviceRepository;
            mongoClient = new MongoClient("mongodb://admin:fukSkNQngNpPG6e@62.112.8.24:27017/AutoLikeV8?authSource=admin");
        }

        public async Task<OrderDto> CancelAsync(Guid id)
        {
            var order = await orderRepository.GetAsync(id);
            if (order == null)
            {
                throw new UserFriendlyException("");
            }

            order.Status = OrderStatus.Pending;
            var updated = await orderRepository.UpdateAsync(order);
            if (updated == null)
            {
                throw new UserFriendlyException("");
            }
            var jobId = await backgroundJobManager.EnqueueAsync(
                new CancelOrderArgs { OrderId = order.Id },
                BackgroundJobPriority.High,
                TimeSpan.FromMinutes(CancelTime));
            return ObjectMapper.Map<Order, OrderDto>(order);

        }

        public async Task<OrderDto> CreateAsync(CreateOrderDto request)
        {
            var service = await serviceRepository.FindAsync(d => d.Id == request.ServiceId);
            if (service == null)
            {
                throw new UserFriendlyException("");
            }
            var speedAvailable = service.Speeds.Any(c => c == request.Speed);
            var warrantyAvailable = service.Warranties.Any(c => c == request.Warranty);
            if (!speedAvailable || !warrantyAvailable)
            {
                throw new UserFriendlyException("");
            }

            var uuid = await uidValidator.GetUidAsync(service.Group, request.Uid);
            if (string.IsNullOrEmpty(uuid))
            {
                throw new UserFriendlyException("");
            }


            var order = new Order(guidGenerator.Create())
            {
                ServiceCode = service.Code,
                User = CurrentUser.ToBase(),
                Status = OrderStatus.Active,
                Quantity = request.Quantity,
                Warranty = request.Warranty,
                Speed = request.Speed,
                Uid = uuid,
                RequestUid = request.Uid,
                Price = request.Quantity * (request.Speed.Price + request.Warranty.Price),
                Service = ObjectMapper.Map<Service, ServiceBase>(service)
            };
            order.Code = codeGenerator.Generate(order.Id, GenerateCode.Order);
            using (var session = mongoClient.StartSession())
            {
                session.StartTransaction();
                var orderResult = await orderRepository.InsertAsync(order);
                if (orderResult == null)
                {
                    session.AbortTransaction();
                    throw new UserFriendlyException("");
                }

                await transactionService.TranferFromUserAsync(CurrentUser.ToBase(), request.Quantity, orderResult, TransactionType.Service);
                session.CommitTransaction();
                return ObjectMapper.Map<Order, OrderDto>(orderResult);
            }
        }

        public async Task<PagedResultDto<OrderDto>> GetOrdersAsync(
            string serviceCode,
            OrderStatus? status,
            DateTime? from,
            DateTime? to,
            int skip = 0,
            int max = 10)
        {
            var query = await orderRepository.GetQueryableAsync();

            var items = query
                .Where(d => d.User.Id == CurrentUser.Id.Value && d.ServiceCode == serviceCode)
                .WhereIf(status.HasValue, d => d.Status == status)
                .WhereIf(from.HasValue, d => d.CreationTime >= from.Value)
                .WhereIf(to.HasValue, d => d.CreationTime <= to.Value)
                .Skip(skip)
                .Take(max);
            var count = items.LongCount();
            return new PagedResultDto<OrderDto>(count, ObjectMapper.Map<List<Order>, List<OrderDto>>(items.ToList()));
        }

        public async Task<OrderDto> ProcessOrderAsync(CreateOrderProcessDto request)
        {
            var order = await orderRepository.GetAsync(request.OrderId);
            if (order == null)
            {
                throw new UserFriendlyException("");
            }
            using (var session = mongoClient.StartSession())
            {
                session.StartTransaction();
                var process = await orderProcessRepository.InsertAsync(new OrderProcess { OrderId = request.OrderId, Quantity = request.Quantity });
                if (process == null)
                {
                    session.AbortTransaction();
                    throw new UserFriendlyException("");
                }
                order.CompletedQuantity += process.Quantity;
                if (order.CompletedQuantity >= order.Quantity)
                {
                    order.Status = OrderStatus.Complete;
                }
                if (order.Processes == null)
                {
                    order.Processes = new OrderProcess[] { process };
                }
                else
                {
                    order.Processes.Add(process);
                }

                var update = await orderRepository.UpdateAsync(order);
                if (update == null)
                {
                    session.AbortTransaction();
                    throw new UserFriendlyException("");
                }
                session.CommitTransaction();
                return ObjectMapper.Map<Order, OrderDto>(update);
            }
        }
    }
}
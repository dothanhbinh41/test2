using AutoLike.Generators;
using AutoLike.Orders.Dtos;
using AutoLike.Services;
using AutoLike.Transactions;
using AutoLike.Users;
using AutoLike.Validators;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Identity;

namespace AutoLike.Orders
{
    public class OrdersAppService : AutoLikeAppService, IOrdersAppService
    {
        public const double CancelTime = 15;//mins
        private readonly IUidValidator uidValidator;
        private readonly ICodeGenerator codeGenerator;
        private readonly IBackgroundJobManager backgroundJobManager;
        private readonly IMongoClient mongoClient;
        private readonly ITransactionService transactionService;
        private readonly IRepository<Order, Guid> orderRepository;
        private readonly IRepository<Transaction, Guid> transactionRepository;
        private readonly IRepository<Service, Guid> serviceRepository;
        private readonly IRepository<IdentityUser, Guid> userRepository;

        public OrdersAppService(
            IUidValidator uidValidator,
            ICodeGenerator codeGenerator,
            IBackgroundJobManager backgroundJobManager,
            IMongoClient mongoClient,
            ITransactionService transactionService,
            IRepository<Order, Guid> orderRepository,
            IRepository<Transaction, Guid> transactionRepository,
            IRepository<Service, Guid> serviceRepository,
            IRepository<IdentityUser, Guid> userRepository)
        {
            this.uidValidator = uidValidator;
            this.codeGenerator = codeGenerator;
            this.backgroundJobManager = backgroundJobManager;
            this.mongoClient = mongoClient;
            this.transactionService = transactionService;
            this.orderRepository = orderRepository;
            this.transactionRepository = transactionRepository;
            this.serviceRepository = serviceRepository;
            this.userRepository = userRepository;
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

            
            var order = new Order
            {   
                User = CurrentUser.ToBase(),
                Status = OrderStatus.Active,
                Quantity = request.Quantity,
                Warranty = request.Warranty,
                Speed = request.Speed,
                Uid = uuid,
                RequestUid = request.Uid,
                Price = request.Quantity * (request.Speed.Price + request.Warranty.Price),
                Service = service
            };
            order.Code = codeGenerator.Generate(order.Id);
            using (var session = mongoClient.StartSession())
            {
                session.StartTransaction();
                var orderResult = await orderRepository.InsertAsync(order);
                if (orderResult == null)
                {
                    session.AbortTransaction();
                    throw new UserFriendlyException("");
                }

                await transactionService.TranferFromUserAsync(CurrentUser.ToBase(), request.Quantity, orderResult, TransactionType.Service, session);
                session.CommitTransaction();
                return ObjectMapper.Map<Order, OrderDto>(orderResult);
            }
        }
    }
}
using AutoLike.Trackings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.ExceptionHandling;

namespace AutoLike.Exceptions
{
    public class AutoLikeExceptionSubscriber : ExceptionSubscriber
    {
        private readonly IRepository<Tracking, Guid> trackingRepository;

        public AutoLikeExceptionSubscriber(IRepository<Tracking, Guid> trackingRepository)
        {
            this.trackingRepository = trackingRepository;
        }

        public override async Task HandleAsync(ExceptionNotificationContext context)
        {
            await trackingRepository.InsertAsync(new Tracking { Message = context.Exception.Message, Data = context.Exception.Data });
        }
    }
}

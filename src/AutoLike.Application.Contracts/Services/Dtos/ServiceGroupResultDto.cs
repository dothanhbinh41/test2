using System;
using System.Collections.Generic;
using System.Text;

namespace AutoLike.Services.Dtos
{
    public class ServiceGroupResultDto
    {
        public ServiceGroup Group { get; set; }
        public ServiceMenuDto[] Services { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace AutoLike.Services
{
    public class ServiceBase
    {
        public string Name { get; set; }
        public string Code { set; get; }
        public ServiceGroup Group { get; set; }
        public ServiceType ServiceType { get; set; }
    }
}

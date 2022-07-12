using System;
using System.Collections.Generic;
using System.Text;

namespace AutoLike.Services.Dtos
{
    public class CreateServiceDto
    {
        public string Name { get; set; } 
        public ServiceGroup Group { get; set; }
        public double Price { get; set; }
    }
}

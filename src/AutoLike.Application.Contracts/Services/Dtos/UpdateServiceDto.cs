using AutoLike.Warranties.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoLike.Services.Dtos
{
    public class UpdateServiceDto
    {
        public string Name { get; set; }
        public ServiceGroup Group { get; set; }
        public double Price { get; set; }
        public WarrantyDto[] Warranties { get; set; }
    }
}

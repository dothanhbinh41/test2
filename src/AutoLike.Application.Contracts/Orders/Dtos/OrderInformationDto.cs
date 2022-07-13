using AutoLike.Services;
using AutoLike.Services.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoLike.Orders.Dtos
{
    public class OrderInformationDto : ServiceDto
    {
        public string UId { get; set; }
        public Warranty Warranty { set; get; }
        public Speed Speed { set; get; }
    }
}

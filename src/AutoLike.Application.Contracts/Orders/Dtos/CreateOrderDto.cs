using AutoLike.Services;
using AutoLike.Users.Dtos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AutoLike.Orders.Dtos
{
    public class CreateOrderDto
    {
        [Required]
        public string UId { get; set; }

        public Warranty Warranty { set; get; }
        public Speed Speed { set; get; }
        public Guid ServiceId { get; set; } 

        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }
    }
}

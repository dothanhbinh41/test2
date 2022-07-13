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
        public Warranty[] Warranties { set; get; }
        public Speed[] Speeds { get; set; }
        public uint MinQuantity { get; set; }
        public uint MaxQuantity { get; set; } = uint.MaxValue;
    }
}

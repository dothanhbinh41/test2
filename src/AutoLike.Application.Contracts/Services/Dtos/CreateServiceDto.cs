using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AutoLike.Services.Dtos
{
    public class CreateServiceDto
    {
        public string Name { get; set; }
        public ServiceGroup Group { get; set; } 
        public Warranty[] Warranties { set; get; }

        [Required]
        [MinLength(1)]
        public Speed[] Speeds { get; set; }

        [Range(1,uint.MaxValue)]
        public uint MinQuantity { get; set; }

        [Range(1, uint.MaxValue)]
        public uint MaxQuantity { get; set; } = uint.MaxValue;
    }
}

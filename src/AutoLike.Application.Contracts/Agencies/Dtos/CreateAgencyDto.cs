using System;
using System.Collections.Generic;
using System.Text;

namespace AutoLike.Agencies.Dtos
{
    public class CreateAgencyDto : RegisterAgencyDto
    {
        public Guid UserId { get; set; } 
    }

    public class RegisterAgencyDto
    { 
        public string AccountName { get; set; }
        public string BankName { get; set; }
        public string BankNumber { get; set; }
        public string Fullname { get; set; }
        public string Domain { get; set; }
        public string Title { get; set; }
        public string Address { get; set; }
        public string Contact { get; set; }
    }
}

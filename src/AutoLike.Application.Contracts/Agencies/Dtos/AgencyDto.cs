using AutoLike.Users.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace AutoLike.Agencies.Dtos
{
    public class AgencyDto : EntityDto<Guid>
    {
        public Guid UserId { get; set; }
        public string PhoneNumber { get; set; }
        public string AccountName { get; set; }
        public string BankName { get; set; }
        public string BankNumber { get; set; }
        public string Fullname { get; set; }
        public string Domain { get; set; }
        public decimal Balance { get; set; }
        public string Title { get; set; }
        public string Address { get; set; }
        public string Contact { get; set; }
        public Guid AgencyKey { set; get; }
        public CommonStatus Status { get; set; }
        public string[] WhiteListIP { get; set; }
        public QRCodeDto QRCode { get; set; }
    }
}

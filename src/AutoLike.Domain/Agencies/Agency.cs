using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace AutoLike.Agencies
{
    public class Agency : FullAuditedAggregateRoot<Guid>
    {
		public Guid UserId { get; set; }
		public string AccountName { get; set; }
		public string BankName { get; set; }
		public string BankNumber { get; set; }
		public string Fullname { get; set; } 
		public string Domain { get; set; } 
		public decimal Balance { get; set; } 
		public string Title { get; set; } 
		public string Address { get; set; } 
		public string Contact { get; set; }  
		public CommonStatus Status { get; set; } 
        public string[] WhiteListIP { get; set; } 
    } 
}

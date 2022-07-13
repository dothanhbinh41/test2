using AutoLike.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLike.Utils
{
    public class CodeGenerator
    {
        public static string GenerateForOrder(Order order)
        {
            var orderCode = UnicodeEncoding.ASCII.GetString(order.Id.ToByteArray());
            var code = $"{order.Info?.Code ?? ""}-{orderCode}";
            order.Code = code;
            return code;
        }

        public static string GenerateForOrder(Order order, Guid id)
        { 
            var orderCode = UnicodeEncoding.ASCII.GetString(id.ToByteArray());
            var code = $"{order.Info?.Code ?? ""}-{orderCode}";
            order.Code = code;
            return code;
        }
    }
}

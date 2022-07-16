using System;
using System.Collections.Generic;
using System.Text;

namespace AutoLike.Orders
{
    public enum OrderStatus
    {
        Active,  // Đang làm việc, hoạt đông
        InActive,  // Đã nghỉ, ngưng hoạt động
        Pending,  // Chờ xử lý
        Complete,
        Delete // Đã xóa
    }
}
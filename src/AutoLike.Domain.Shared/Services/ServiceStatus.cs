using System;
using System.Collections.Generic;
using System.Text;

namespace AutoLike.Services
{
    public enum ServiceStatus
    {
        Active,  // Đang làm việc, hoạt đông
        InActive,  // Đã nghỉ, ngưng hoạt động
        Pending,  // Chờ xử lý
        Delete // Đã xóa
    }
}
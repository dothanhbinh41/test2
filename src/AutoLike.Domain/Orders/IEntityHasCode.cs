﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLike.Orders
{
    public interface ITransactionInformation
    {
        Guid Id { get; }
        string Code { set; get; } 
    }
}

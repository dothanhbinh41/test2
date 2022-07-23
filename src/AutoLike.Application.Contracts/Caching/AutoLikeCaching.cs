﻿using System;
using System.Collections.Generic;
using System.Text;

namespace AutoLike.Caching
{
    public class AutoLikeCaching
    {
        public const string Get = $"Get";
        public const string GetList = $"GetList";
        public const string ServiceCacheGroup = $"Service";
        public static TimeSpan TimeExpried = TimeSpan.FromDays(1);


        public static string GetCache(Guid id, string group, string method) => $"{group}.{method}.{id}";
        public static string GetCache(Guid id, string group) => $"{group}.{Get}.{id}";
        public static string GetListCache(int skip, string group) => $"{group}.{GetList}.{skip}";
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLike.Generators
{
    public class CodeGenerator : ICodeGenerator
    {
        public string Generate(Guid uid)
        { 
            return uid.ToString().Split('-').Last();
        }

        public string Generate(string code, int number)
        {
            var cache = new Dictionary<int, string>();
            return Generate(number, cache);
        }

        string Generate(int num, Dictionary<int, string> cache)
        {
            if (cache.ContainsKey(num))
            {
                return cache[num];
            }
            var str = "SKWGHXA07DU2Z51C9V68IQLFNTPBO34YJRME";
            var result = string.Empty;
            if (num > 0)
            {
                var mod = num % str.Length;

                var newNum = num / str.Length;
                var c = str[mod];

                result = string.Format("{0}{1}{2}", Generate(newNum, cache), c, result);
                cache[num] = result;

            }
            return result;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility
{
    public static class Pagination
    {
        public static IEnumerable<TSource> ToPage<TSource>(this IEnumerable<TSource> sources, int page,int pagesize,out int rowCount)
        {
            rowCount = sources.Count();
            return sources.Skip((page - 1) * pagesize).Take(pagesize);
        }
    }
}

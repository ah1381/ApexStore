using System.Diagnostics;
using System.Linq;
using System.Collections.Generic;


namespace ApexStore.Common
{
    public static class Pageination
    {
        public static  IEnumerable<TraceSource> Topaged<TraceSource>(this IEnumerable<TraceSource> source,int page, int pageSize,out int rowsCount)
        {
            rowsCount = source.Count();
            return source.Skip((page - 1) * pageSize).Take(pageSize);
        }
    }
}

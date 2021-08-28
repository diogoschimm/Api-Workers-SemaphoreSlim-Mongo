using AppServerThread.Core;
using System;
using System.Collections.Generic;

namespace AppServerThread.WebApi.Utils
{
    public static class PaggingUtils
    { 
        public static int GetTotalPages<T>(IList<T> results, Options options)
        {
            if (options.Total > results.Count)
                options.Total = results.Count;

            var rest = results.Count % options.Total;
            var division = (decimal)results.Count / options.Total;

            if (rest == 0)
                return (int)division;

            return (int)(Math.Truncate(division)) + 1;
        }
    }
}

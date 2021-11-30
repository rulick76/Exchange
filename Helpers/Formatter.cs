using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Exchange.Helpers
{
    public class Formatter
    {
        public static string FormatCsv(string response)
        {
            response = ReplaceTokens(response);
            return response;
        }

        private static string ReplaceTokens(string response)
        {
            response = response.Replace("[", string.Empty);
            response = response.Replace("]", string.Empty);
            response = response.Replace("{", string.Empty);
            response = response.Replace("}", "\n");
            return response;
        }
    }
}

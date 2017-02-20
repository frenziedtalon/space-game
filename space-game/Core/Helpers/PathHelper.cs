using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Helpers
{
    public class PathHelper
    {
        public static string SitePathCombine(string path1, string path2)
        {

            if (new List<string> () { 
                path1,
				path2
            }.Any(p => string.IsNullOrWhiteSpace(p))) {
                throw new ArgumentNullException();
            }

            StringBuilder result = new StringBuilder();

            result.Append(path1.TrimEnd('/'));
            result.Append("/");
            result.Append(path2.TrimStart('/'));

            return result.ToString();
        }

    }
}

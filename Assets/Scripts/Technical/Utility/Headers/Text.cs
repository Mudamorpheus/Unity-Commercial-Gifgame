using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace LTG.Technical
{
    public class Text
    {
        //Starts and ends with
        public static bool EdgesWith(string str, string start, string end)
        {
            return str.StartsWith(start) && str.EndsWith(end);
        }

        //Trim start
        public static string TrimStart(string str, string trim)
        {
            if (!string.IsNullOrEmpty(str) && str.StartsWith(trim))
            {
                str = str.Substring(trim.Length, str.Length - trim.Length);
            }

            return str;
        }

        //Trim end
        public static string TrimEnd(string str, string trim)
        {
            if (!string.IsNullOrEmpty(str) && str.EndsWith(trim))
            {
                str = str.Substring(0, str.Length - trim.Length);
            }

            return str;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Funemag.Helpers
{
    public static class Truncate
    {
        public static string TrucateHtml(string input, string marker)
        {
            int stopIndex = FindNthOccuranceIndex(3, input, marker);
            if (FindNthOccuranceIndex(3, input, marker) > 0)
                return input.Substring(0, stopIndex);
            else
                return input;
        }

        public static int FindNthOccuranceIndex(int n, string input, string marker)
        {
            int index = 0;

            for(int i = 0; i < n; i++)
            {
                int tempIndex = input.IndexOf(marker) + marker.Length;
                input = input.Substring(tempIndex);

                // Check that the Nth occurance exists
                if (tempIndex == -1)
                    break;
                else
                {
                    index += tempIndex;
                    if (index > 1000)
                        break;
                }
            }

            return index;
        }
    }
}
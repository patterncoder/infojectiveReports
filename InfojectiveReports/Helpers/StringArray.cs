using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InfojectiveReports
{
    public static class StringArray
    {
        public static string[] GetParameters(string inParam)
        {
            string[] newParams = inParam.Split(';');
            return newParams;
        }

        public static int[] GetWidths(string widths)
        {
            string[] array = widths.Split(';');
            int length = array.Length;
            int[] newArray = new int[length];
            for (int i=0;i<length;i++)
            {
                newArray[i] = int.Parse(array[i]);
            }

            return newArray;
        }
        
              
    }
}

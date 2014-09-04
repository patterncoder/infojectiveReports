using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InfojectiveReports
{
    public class BindingParser
    {

        public string Binding;
        public string Field;
        public string Beginnning;
        public string Ending;
        public string Result;

        public BindingParser(string binding)
        {
            Binding = binding;
        }

        public string GetField()
        {
            int start = Binding.IndexOf("{");
            int end = Binding.IndexOf("}");
            int lastChar = Binding.Length;
            Beginnning = Binding.Substring(0, start);
            Ending = Binding.Substring(end + 1, lastChar - end - 1);
            Field = Binding.Substring(start + 1, end - start - 1);
            return Field;
        }
        
        public string InjectData(string data)
        {

            
            
            Result = Beginnning + data + Ending;
                return Result;
        }
    }

    
}

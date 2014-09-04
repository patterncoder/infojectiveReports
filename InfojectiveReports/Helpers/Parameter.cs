using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InfojectiveReports
{
    public class Parameter : IParameter
    {

        private string _Name;

        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }

        private string _ParameterType;

        public string ParameterType
        {
            get { return _ParameterType; }
            set { _ParameterType = value; }
        }

        private string _Value;

        public string Value
        {
            get { return _Value; }
            set { _Value = value; }
        }

        private string _ValidationText;

        public string ValidationText
        {
            get { return _ValidationText; }
            set { _ValidationText = value; }
        }
        
        

    }
}

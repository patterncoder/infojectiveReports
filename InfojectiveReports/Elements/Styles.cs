using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InfojectiveReports
{
    public class TagStyle : IStyle
    {


        private string _Name;

        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }

        private string _Property;

        public string Property
        {
            get { return _Property; }
            set { _Property = value; }
        }

        private string _Value;

        public string Value
        {
            get { return _Value; }
            set { _Value = value; }
        }
        
        
    }

    public class ClassStyle : IStyle
    {
        private string _Name;

        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }

        private string _Property;

        public string Property
        {
            get { return _Property; }
            set { _Property = value; }
        }

        private string _Value;

        public string Value
        {
            get { return _Value; }
            set { _Value = value; }
        }
    }


    
}

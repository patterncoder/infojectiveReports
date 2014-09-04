using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InfojectiveReports
{
    public interface IParameter
    {
        string Name {get;set;}
        string ParameterType { get; set; }
        string Value { get; set; }
        string ValidationText { get; set; }

    }
}

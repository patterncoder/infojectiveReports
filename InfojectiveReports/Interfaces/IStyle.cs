using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InfojectiveReports
{
    public interface IStyle
    {
        string Name { get; set; }
        string Property { get; set; }
        string Value { get; set; }
    }
}

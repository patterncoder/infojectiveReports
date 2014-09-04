using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InfojectiveReports
{
    public interface ITextItem: IReportItem
    {
        string Text { get; set; }
    }
}

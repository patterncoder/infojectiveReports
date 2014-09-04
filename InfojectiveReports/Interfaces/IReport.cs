using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using iTextSharp.text;

namespace InfojectiveReports
{
    public interface IReport
    {
        string Name { get; set; }
        string Title { get; set; }
        string Orientation { get; set; }
        string PageEvent { get; set; }
        string Description { get; set; }
        string Connection { get; set; }
        float MarginTop { get; set; }
        float MarginBottom { get; set; }
        float MarginLeft { get; set; }
        float MarginRight { get; set; }
        Rectangle PageSize { get; set; }

        List<IReportItem> ReportItems { get; set; }



    }
}

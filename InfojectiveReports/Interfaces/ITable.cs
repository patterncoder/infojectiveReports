using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using iTextSharp.text.pdf;

namespace InfojectiveReports
{
    public interface ITable : IReportItem
    {
        int ColumnCount { get; set; }
        float TableWidth { get; set; }
        int TableAlign { get; set; }
        int[] ColumnWidths { get; set; }
        List<ITableRow> Rows { get; set; }
        float SpacingBefore { get; set; }
        float SpacingAfter { get; set; }
        


        


    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InfojectiveReports
{
    public interface ITableRow
    {
        List<ITableCell> RowCells { get; set; }
    }
}

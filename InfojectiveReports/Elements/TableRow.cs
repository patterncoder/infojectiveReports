using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InfojectiveReports
{
    class TableRow : ITableRow
    {
        private List<ITableCell> _RowCells;
        public List<ITableCell> RowCells
        {
            get
            {
                return _RowCells;
            }
            set
            {
                _RowCells = value;
            }
        }
    }
}

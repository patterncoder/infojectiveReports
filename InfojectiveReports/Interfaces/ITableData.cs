using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InfojectiveReports
{
    public interface ITableData : ITable
    {
        string Connection { get; set; }
        string DataSource { get; set; }

    }
}

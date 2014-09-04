using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace InfojectiveReports
{
    public interface IReportController
    {
        
        MemoryStream CreatePDF(IReport report, List<IParameter> parameters);


    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InfojectiveReports
{
    public interface IReportParameters
    {
        List<IParameter> GetParameters(string reportTemplatePath);
    }
}

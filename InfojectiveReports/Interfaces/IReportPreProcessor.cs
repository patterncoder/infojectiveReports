using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Text;

namespace InfojectiveReports
{
    public interface IReportPreProcessor
    {

        IReport PreProcess(string templatePath, List<IParameter> parameters);
    }
}

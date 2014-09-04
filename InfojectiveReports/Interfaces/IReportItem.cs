using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using iTextSharp.text;


namespace InfojectiveReports
{
    public interface IReportItem
    {
        string TagName { get; set; }
        string Name { get; set; }
        Boolean Visible { get; set; }
        string XMLConfig { get; set; }
        IElement GetPDFElement();
        


    }
}

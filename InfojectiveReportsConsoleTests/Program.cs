using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InfojectiveReports;

using System.IO;
using System.Diagnostics;

namespace InfojectiveReportsConsoleTests
{
    class Program
    {
        static void Main(string[] args)
        {
            
            FillParameters();
            
        }

        private static void FillParameters()
        {
            List<IParameter> reportParams = ReportParameters.GetParameters(@"C:\Users\patterncoder\Documents\Visual Studio 2010\Projects\InfojectiveReports2\InfojectiveReportsConsoleTests\Reports\EmpSalesReport.xml");
            foreach (IParameter param in reportParams)
            {
                Console.WriteLine(param.Name + ' ' + param.ParameterType + ' ' + param.ValidationText);
                param.Value = Console.ReadLine();
            }
            RunReport(reportParams);
            //foreach (IParameter param in reportParams)
            //{
            //    Console.WriteLine(param.Value);
            //}
            //Console.ReadLine();
        }

        private static void RunReport(IList<IParameter> listParams)
        {
            string path = @"C:\Users\patterncoder\Documents\Visual Studio 2010\Projects\InfojectiveReports2\InfojectiveReportsConsoleTests\Reports\EmpSalesReport.xml";
            Dictionary<string, string> myDic = new Dictionary<string, string>();
            foreach (IParameter listParam in listParams)
            {
                myDic.Add(listParam.Name, listParam.Value);
            }
            //myDic.Add("@EndDateDate", "200");
            //IReport report = new ReportPreProcessor().PreProcess(path,myDic);
            IReportController reportController = new ReportController();
            IReport myReport = new ReportPreProcessor().PreProcess(path, myDic);
            byte[] content = reportController.CreatePDF(myReport).ToArray();
            using (FileStream fs = File.Create(@"c:\aTestFile5.pdf"))
            {
                fs.Write(content, 0, (int)content.Length);
            }

            Process.Start("AcroRd32.exe", @"c:\aTestFile5.pdf");

            //string batchFileName = "temp_pdflauncher.bat";
 
            //using (StreamWriter sw = new StreamWriter(batchFileName, false))
 
            //    {
 
            //        sw.WriteLine("call \"" + @"c:\aTestFile5.pdf" + "\"");
 
            //    }
 
            //Process.Start("cmd", "/c " + batchFileName);


        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace InfojectiveReports
{
    public class ReportController : IReportController
    {



        public MemoryStream CreatePDF(IReport report, List<IParameter> parameters)
        {
            
            // Document Setup
            // 
            
            MemoryStream PDFData = new MemoryStream();
            Document document = new Document();
            if (report.Orientation == "Portrait")
            {
                document.SetPageSize(report.PageSize);
            }
            if (report.Orientation == "Landscape")
            {
                document.SetPageSize(report.PageSize.Rotate());
             
            }
            
            document.SetMargins(report.MarginLeft, report.MarginRight, report.MarginTop, report.MarginBottom);
                
            PdfWriter PDFWriter = PdfWriter.GetInstance(document, PDFData);
            PDFWriter.ViewerPreferences = PdfWriter.PageModeUseOutlines;

            // Our custom Header and Footer is done using Event Handler

            //PdfPageEventHelper PageEventHandler;

            if (report.PageEvent == "TwoColumnHeaderFooter")
            {
                TwoColumnHeaderFooter PageEventHandler = new TwoColumnHeaderFooter();
                PDFWriter.PageEvent = PageEventHandler;
                // Define the page header
                PageEventHandler.Title = report.Title;
                PageEventHandler.HeaderFont = FontFactory.GetFont(BaseFont.COURIER_BOLD, 10, Font.BOLD);
                PageEventHandler.HeaderLeft = report.Name;
                PageEventHandler.HeaderRight = report.Description;
                PDFWriter.PageEvent = PageEventHandler;
            }

            if (report.PageEvent == "RecipeHeader")
            {
                RecipeHeader PageEventHandler = new RecipeHeader();
                PDFWriter.PageEvent = PageEventHandler;
                // Define the page header
                PageEventHandler.Title = report.Title;
                PageEventHandler.HeaderFont = FontFactory.GetFont(BaseFont.COURIER_BOLD, 10, Font.BOLD);
                PageEventHandler.HeaderLeft = report.Name;
                PageEventHandler.HeaderRight = report.Description;
                PDFWriter.PageEvent = PageEventHandler;
            }
                
               
            
            

            // Define the page header
            //PageEventHandler.Title = report.Title;
            //PageEventHandler.HeaderFont = FontFactory.GetFont(BaseFont.COURIER_BOLD, 10, Font.BOLD);
            //PageEventHandler.HeaderLeft = report.Name;
            //PageEventHandler.HeaderRight = report.Description;
            document.Open();
            foreach (IReportItem rptItem in report.ReportItems)
            {
                
                if (rptItem.GetType().ToString() == "InfojectiveReports.HTMLSnippet")
                {
                    
                    HTMLSnippet snip = new HTMLSnippet();
                    snip = (HTMLSnippet)rptItem;
                   
                    snip.CreateSnippet(document);
                }
                else
                { 
                    
                    document.Add(rptItem.GetPDFElement());
                }
                
                
            }
            
            document.Close();
            return PDFData;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.IO;

using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.draw;

namespace InfojectiveReports
{
    public class ReportController2
    {
        public MemoryStream CreatePDF(string reportPath)
        {
            return new MemoryStream();
        }

        public MemoryStream CreatePDF(string reportPath, Dictionary<string, string> parameters)
        {
           //open xml document of supplied path
            XDocument report = XDocument.Load(reportPath);

            //get the parameters from the dictionary coming in placed into the queries in the document
            foreach (XElement param in report.Descendants("Element"))
            {
                if ((string)param.Attribute("parameters") != null)
                {
                    foreach (string strParam in StringArray.GetParameters(param.Attribute("parameters").Value))
                    {
                        param.Attribute("datasource").Value = param.Attribute("datasource").Value.Replace(strParam, parameters[strParam]);
                    }
                }
            }

            #region PageSettings
           

           
            // Get Page Settings
            string _name = report.Root.Element("Head").Element("Name").Value;
            string _title = report.Root.Element("Head").Element("Title").Value;
            string _description = report.Root.Element("Head").Element("Description").Value;
            string _connection = report.Root.Element("Head").Element("Connection").Value;
            float _margin_top = Convert.ToSingle(report.Root.Element("Layout").Element("Margin-Top").Value);
            float _margin_right = Convert.ToSingle(report.Root.Element("Layout").Element("Margin-Right").Value);
            float _margin_bottom = Convert.ToSingle(report.Root.Element("Layout").Element("Margin-Bottom").Value);
            float _margin_left = Convert.ToSingle(report.Root.Element("Layout").Element("Margin-Left").Value);
            Rectangle _pageSize;

            switch (report.Root.Element("Layout").Element("Page-Size").Value)
            {
                
                case "Letter":
                    _pageSize = PageSize.LETTER;
                    break;
                case "Legal":
                    _pageSize = PageSize.LEGAL;
                    break;
                default:
                    _pageSize = PageSize.LETTER;
                    break;
            }          
            #endregion



            // process xml document by creating a new pdf memory stream

            MemoryStream PDFData = new MemoryStream();
            Document document = new Document(_pageSize, _margin_left, _margin_right, _margin_top, _margin_bottom);
            PdfWriter PDFWriter = PdfWriter.GetInstance(document, PDFData);
            PDFWriter.ViewerPreferences = PdfWriter.PageModeUseOutlines;

            // Our custom Header and Footer is done using Event Handler
            TwoColumnHeaderFooter PageEventHandler = new TwoColumnHeaderFooter();
            PDFWriter.PageEvent = PageEventHandler;

            // Define the page header
            PageEventHandler.Title = _title;
            PageEventHandler.HeaderFont = FontFactory.GetFont(BaseFont.COURIER_BOLD, 10, Font.BOLD);
            PageEventHandler.HeaderLeft = _name;
            PageEventHandler.HeaderRight = _description;


            document.Open();

            

            // Create new element generator  for following iteration through the passed in elements
            ElementGenerator eg = new ElementGenerator(_connection);

            foreach (XElement xe in report.Root.Element("Elements").Elements("Element"))
            {
                switch (xe.Attribute("type").Value)
                { 
                    case "table":
                        
                        document.Add(eg.CreateSimpleTable(xe));
                    break;
                    case "text":
                    document.Add(eg.CreateParagraph(xe));
                    break;
                }
                
                
            }
            
            document.Close();
            return PDFData;
        }

    }
}

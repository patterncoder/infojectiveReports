using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Xml.Linq;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.draw;

/// <summary>
/// Summary description for otd_report_table
/// </summary>
namespace InfojectiveReports

{
    public class ElementGenerator
    {
        private string _connection;
        public ElementGenerator(string conn)
        {
            _connection = conn;
        }
        

        public PdfPTable CreateSimpleTable(XElement xe)
        {

         
            DBConnHelper helper = new DBConnHelper(_connection);
            DataTable myTable = helper.GetDataTableSQL(xe.Attribute("datasource").Value);
            
            PdfPTable newTable = new PdfPTable(xe.Element("Columns").Elements("Column").Count());
            
            
           
            foreach (DataRow row in myTable.Rows)
            {
                foreach (XElement col in xe.Element("Columns").Elements("Column"))
                {
                    if (col.HasElements)
                    {
                        foreach (XElement xe2 in col.Descendants("Element"))
                        {
                            switch (xe2.Attribute("type").Value)
                            {
                                case "table":
                                    newTable.AddCell(CreateSimpleTable(xe2));
                                    break;
                                case "text":
                                    newTable.AddCell(CreateParagraph(xe2));
                                    break;
                            }
                         }
                    }
                    else
                    {
                        Font newFont = new Font(Font.FontFamily.TIMES_ROMAN);
                        PdfPCell newCell = new PdfPCell(new Phrase(row[col.Attribute("name").Value].ToString(),newFont));
                        //newCell.Border = 1;
                        
                        newTable.AddCell(newCell);
                    }
                }
                
            }
            
            return newTable;
        }

        public Paragraph CreateParagraph(XElement xe)
        {
            Paragraph newPara = new Paragraph(xe.Value);
            return newPara;
        }

   

    }
}
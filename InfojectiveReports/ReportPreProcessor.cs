using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Linq.Expressions;
using iTextSharp;
using iTextSharp.text;


namespace InfojectiveReports
{
    public class ReportPreProcessor : IReportPreProcessor
    {

        private IReport _report = new Report();
        private XDocument _xmlReport;
        private List<IParameter> _parameters;

        /// <summary>
        /// Pass in an XML document and get back an object model of the report template.
        /// </summary>
        /// <param name="templatePath"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public IReport PreProcess(string templatePath, List<IParameter> parameters)
        {

            _parameters = parameters;
            _xmlReport = XDocument.Load(templatePath);
            XNamespace ns = _xmlReport.Root.Name.Namespace;
            LoadParameters(parameters);
            DocumentSetup();
            //Add Report Header and Footer
            //Add Page Header and Footer
            AddReportItems(_xmlReport.Root.Element(ns + "Body"));
            
            
            //_xmlReport.Save(@"c:\anewTestDocument.xml");
            return _report;
        }

        private void AddReportItems(XElement body)
        {
            
            _report.ReportItems = new List<IReportItem>();
           
            foreach (XElement reportTemplateItem in body.Descendants())
            {

                //XNamespace ns = reportTemplateItem.Name.Namespace;
                IReportItem newItem;
                
                switch (reportTemplateItem.Name.LocalName)
                {
	                   case "DataTable":
                            //Console.WriteLine("This is the table");
                            newItem = new TableDataSQL(reportTemplateItem,_report.Connection,_parameters);
                            _report.ReportItems.Add(newItem);
                            break;
                        case "Text":
                            //Console.WriteLine("This is the text");
                            newItem = new TextSimple(reportTemplateItem);
                            _report.ReportItems.Add(newItem);
                            break;
                        case "Image":
                            newItem = new ReportImage(reportTemplateItem);
                            _report.ReportItems.Add(newItem);
                            break;
                    case "HTMLSnippet" :
                            newItem = new HTMLSnippet(reportTemplateItem, _report.Connection,_parameters);
                            _report.ReportItems.Add(newItem);
                            break;
                        default:
                            break;
                }

                
                

               
                
               
            }
            //Console.ReadLine();
        }

        private void DocumentSetup()
        {

            
            #region PageSettings
            XNamespace ns = _xmlReport.Root.Name.Namespace;
            // Get Page Settings
            _report.Name = _xmlReport.Root.Element(ns + "Head").Element(ns + "Name").Value;
            _report.Title = _xmlReport.Root.Element(ns + "Head").Element(ns + "Title").Value;
            _report.Description = _xmlReport.Root.Element(ns + "Head").Element(ns + "Description").Value;
            _report.Connection = _xmlReport.Root.Element(ns + "Head").Element(ns + "Connection").Value;
            _report.MarginTop = Convert.ToSingle(_xmlReport.Root.Element(ns + "Layout").Element(ns + "Margin-Top").Value);
            _report.MarginRight = Convert.ToSingle(_xmlReport.Root.Element(ns + "Layout").Element(ns + "Margin-Right").Value);
            _report.MarginBottom = Convert.ToSingle(_xmlReport.Root.Element(ns + "Layout").Element(ns + "Margin-Bottom").Value);
            _report.MarginLeft = Convert.ToSingle(_xmlReport.Root.Element(ns + "Layout").Element(ns + "Margin-Left").Value);
            _report.Orientation = _xmlReport.Root.Element(ns + "Layout").Element(ns+"Orientation").Value;

            

            if (_xmlReport.Root.Element(ns+ "Layout").Descendants(ns + "PageEvent").Any())
            {
                _report.PageEvent = _xmlReport.Root.Element(ns + "Layout").Element(ns + "PageEvent").Value;
            }
            


            switch (_xmlReport.Root.Element(ns + "Layout").Element(ns + "Page-Size").Value)
            {

                case "Letter":
                    _report.PageSize = PageSize.LETTER;
                    break;
                case "Legal":
                    _report.PageSize = PageSize.LEGAL;
                    break;
                default:
                    _report.PageSize = PageSize.LETTER;
                    break;
            }
            #endregion
        }

        private void LoadParameters(List<IParameter> parameters)
        {
            var items = (from r in _xmlReport.Descendants()
                         where r.Attribute("DataSource") != null && r.Attribute("DataSource").Value.Contains("@")
                         select r);

            foreach (XElement reportItem in items)
            {
                foreach (IParameter param in parameters)
                {
                    if (reportItem.Attribute("DataSource").Value.Contains(param.Name))
                    {
                        reportItem.Attribute("DataSource").Value = reportItem.Attribute("DataSource").Value.Replace(param.Name, param.Value);
                    }
                }
            }

            

            //foreach (XElement reportItem in _xmlReport.Descendants())
            //{
            //    if ((string)reportItem.Attribute("datasource") != null && reportItem.Attribute("datasource").Value.Contains("@"))
            //    {
            //        foreach (KeyValuePair<string, string> pair in parameters)
            //        {
            //            if (reportItem.Attribute("datasource").Value.Contains(pair.Key))
            //            {
            //                reportItem.Attribute("datasource").Value = reportItem.Attribute("datasource").Value.Replace(pair.Key, pair.Value);
            //            }
            //        }
            //    }
                
            //}
                //{
                //    if ((string)reportItem.Attribute("parameters") != null)
                //    {
                //        foreach (string strParam in StringArray.GetParameters(reportItem.Attribute("parameters").Value))
                //        {
                //            reportItem.Attribute("datasource").Value = reportItem.Attribute("datasource").Value.Replace(strParam, parameters[strParam]);
                //        }
                //    }
                //}
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using iTextSharp.text;
using System.Data;
using iTextSharp.text.html.simpleparser;
using System.IO;
using iTextSharp.text.html;



namespace InfojectiveReports
{
    public class HTMLSnippet : IReportItem
    {
        private XElement _xmlConfig;
        private List<IParameter> _parameters;
        private XNamespace _xNamespace;
        public HTMLSnippet(XElement reportItem, string conn, List<IParameter> parameters)
        {
            _Connection = conn;
            _xmlConfig = reportItem;
            _parameters = parameters;
            _xNamespace = reportItem.Name.Namespace;
            FillFromXML(reportItem);
        }

        public HTMLSnippet() { }

        private DataTable _Data;

        public DataTable Data
        {
            get { return _Data; }
            set { _Data = value; }
        }
        
        
        private string _Connection;

        public string Connection
        {
            get { return _Connection; }
            set { _Connection = value; }
        }
        
        
        private string _DataSource;
        public string DataSource
        {
            get { return _DataSource; }
            set { _DataSource = value; }
        }

        private List<IStyle> _Styles;

        public List<IStyle> Styles
        {
            get { return _Styles; }
            set { _Styles = value; }
        }
        


        public string TagName
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public string Name
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public bool Visible
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        
        public string XMLConfig
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public iTextSharp.text.IElement GetPDFElement()
        {
            return new Phrase("hello");
        } 
        
        private void FillFromXML(XElement reportItem)
        {
            XNamespace ns = reportItem.Name.Namespace;
            
            if (reportItem.Attribute("DataSource") != null)
            {
                _DataSource = reportItem.Attribute("DataSource").Value;
            }

            _Data = new DBConnHelper(_Connection,_parameters).GetDataTableSQL(_DataSource);

            List<IStyle> newStyles = new List<IStyle>();
            foreach (XElement rptStyle in reportItem.Element(ns + "StyleSheet").Descendants())
            {
                
                if (rptStyle.Name.LocalName == "ClassStyle")
                {
                    IStyle newStyle = new ClassStyle();
                    newStyle.Name = rptStyle.Attribute("ClassName").Value;
                    newStyle.Property = rptStyle.Attribute("Property").Value;
                    newStyle.Value = rptStyle.Attribute("Value").Value;
                    newStyles.Add(newStyle);
                }
                if (rptStyle.Name.LocalName == "TagStyle")
                {
                    IStyle newStyle = new TagStyle();
                    newStyle.Name = rptStyle.Attribute("TagName").Value;
                    newStyle.Property = rptStyle.Attribute("Property").Value;
                    newStyle.Value = rptStyle.Attribute("Value").Value;
                    newStyles.Add(newStyle);
                }
                _Styles = newStyles;
                

            }

        }

        public void CreateSnippet(Document doc)
        {

            StyleSheet styles = new StyleSheet();

            // Load iText stylesheet from xml data
            LoadStyleSheet(styles);

            // Loop through data
            foreach (DataRow row in _Data.Rows)
            {
                
                //known to work: leading, face, times-roman, ul, li, indent, i, b, color
                //created SpacingBefore and SpacingAfter

                foreach (XElement element in _xmlConfig.Element(_xNamespace + "Elements").Descendants())
                {
                    if (element.Name.LocalName == "Paragraph")
                    {
                        BindingParser myParser = new BindingParser(element.Attribute("Binding").Value);
                        Paragraph myPara = new Paragraph(myParser.InjectData(row[myParser.GetField()].ToString()));
                        if (element.Attribute("SpacingAfter") != null)
                        {
                            myPara.SpacingAfter = Convert.ToSingle( element.Attribute("SpacingAfter").Value);
                        }

                        doc.Add(myPara);
                    }
                    if (element.Name.LocalName == "Snippet")
                    {

                        BindingParser myParser = new BindingParser(element.Attribute("Binding").Value);

                        string contents = row[myParser.GetField()].ToString();
                        var parsedHtmlElements = HTMLWorker.ParseToList(new StringReader(contents), styles);

                        foreach (var htmlElement in parsedHtmlElements)
                        {
                           
                            switch (htmlElement.GetType().ToString())
                            {
                                case "iTextSharp.text.Paragraph":
                                    doc.Add(StyleParagraph(htmlElement));
                                    break;
                                case "iTextSharp.text.List":
                                    doc.Add(StyleList(htmlElement,0));
                                    break;
                                default:
                                    doc.Add(htmlElement);
                                    break;
                                    
                            }
                               
                           
                        }
                    }
                }
                
                
               

            }
            

        }

        private IElement StyleList(IElement htmlElement, int depth)
        {
            depth++;
            List myList = new List();
            myList = (List)htmlElement;
            myList.Autoindent = true;
            myList.IndentationLeft =  (depth * 10);
            

            
            foreach (var item in myList.Items)
            {

                switch (item.GetType().ToString())
                { 
                    case "iTextSharp.text.ListItem":
                        StyleListItem(item, depth);
                        break;
                    case "iTextSharp.text.List":
                        StyleList(item, depth);
                        break;
                    default:
                        break;
                }
                
                
            }
            return myList;
            
        }

        private void StyleListItem(IElement item, int depth)
        {
            ListItem newListItem = (ListItem)item;

            //newListItem.IndentationLeft =  (depth * 10);
            
            
            if (_Styles.Any(u => u.Name == "li" & u.Property == "SpacingBefore"))
            {
                newListItem.SpacingBefore = Convert.ToSingle((from e in _Styles where e.Name == "li" & e.Property == "SpacingBefore" select e.Value).FirstOrDefault());
            }
            if (_Styles.Any(u => u.Name == "li" & u.Property == "SpacingAfter"))
            {
                newListItem.SpacingAfter = Convert.ToSingle((from e in _Styles where e.Name == "li" & e.Property == "SpacingAfter" select e.Value).FirstOrDefault());
            };
        }

        private IElement StyleParagraph(IElement htmlElement)
        {
            Paragraph myPara = new Paragraph();
            myPara = (Paragraph)htmlElement;


            if (_Styles.Any(u => u.Property == "SpacingAfter" & u.Name == "p"))
            {
                myPara.SpacingAfter = Convert.ToSingle((from e in _Styles where e.Name == "p" & e.Property == "SpacingAfter" select e.Value).First());
            }
            if (_Styles.Any(u => u.Property == "SpacingBefore" & u.Name == "p"))
            {
                myPara.SpacingBefore = Convert.ToSingle((from e in _Styles where e.Name == "p" & e.Property == "SpacingBefore" select e.Value).First());
            }
            
            return myPara;
        }

        private void LoadStyleSheet(StyleSheet styles)
        {
            if (_Styles != null)
            {
                foreach (IStyle itemStyle in _Styles)
                {
                    if (itemStyle.GetType().ToString() == "InfojectiveReports.TagStyle")
                    {
                        styles.LoadTagStyle(itemStyle.Name, itemStyle.Property, itemStyle.Value);
                    }
                    if (itemStyle.GetType().ToString() == "InfojectiveReports.ClassStyle")
                    {
                        styles.LoadStyle(itemStyle.Name, itemStyle.Property, itemStyle.Value);
                    }
                }
            }
        }
    }
}

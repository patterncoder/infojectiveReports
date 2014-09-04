using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using iTextSharp.text;
using System.Xml.Linq;
using System.IO;
using System.Web;





namespace InfojectiveReports
{
    public class ReportImage : IReportItem
    {

        public ReportImage(XElement reportItem)
        {
            _XMLConfig = reportItem.ToString();
            FillFromXML(reportItem);
        }

        private void FillFromXML(XElement reportItem)
        {
            if (reportItem.Attribute("Source") != null)
            {
                _Source = reportItem.Attribute("Source").Value;
            }
            if (reportItem.Attribute("PositionX") != null)
            {
                _PositionX = Convert.ToSingle(reportItem.Attribute("PositionX").Value) ;
            }
            if (reportItem.Attribute("PositionY") != null)
            {
                _PositionY = Convert.ToSingle(reportItem.Attribute("PositionY").Value) ;
            }
            if (reportItem.Attribute("PercentScaleX") != null)
            {
                _PercentScaleX = Convert.ToSingle(reportItem.Attribute("PercentScaleX").Value);
            }
            if (reportItem.Attribute("PercentScaleY") != null)
            {
                _PercentScaleY = Convert.ToSingle(reportItem.Attribute("PercentScaleY").Value);
            }
        }
        
        private string _TagName;

        public string TagName
        {
            get { return _TagName; }
            set { _TagName = value; }
        }

        
        
        private string _Name;

        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }

        private Boolean _Visible;

        public Boolean Visible
        {
            get { return _Visible; }
            set { _Visible = value; }
        }


        private float _PositionX = 0;

        public float PositionX
        {
            get { return _PositionX; }
            set { _PositionX = value; }
        }
        private float _PositionY =0;

        public float PositionY
        {
            get { return _PositionY; }
            set { _PositionY = value; }
        }
        private float _PercentScaleX =100;

        public float PercentScaleX
        {
            get { return _PercentScaleX; }
            set { _PercentScaleX = value; }
        }
        private float _PercentScaleY = 100;

        public float PercentScaleY
        {
            get { return _PercentScaleY; }
            set { _PercentScaleY = value; }
        }
        
        
        
        
        private string _Source;

        public string Source
        {
            get { return _Source; }
            set { _Source = value; }
        }
        

        private string _XMLConfig;

        public string XMLConfig
        {
            get { return _XMLConfig; }
            set { _XMLConfig = value; }
        }

        public IElement GetPDFElement()
        {

            string path;
            if (HttpContext.Current == null)
            {
                path = _Source;
            }
            else
            { 
                path = HttpContext.Current.Server.MapPath(_Source);
            }
            
           
            
            var logo = iTextSharp.text.Image.GetInstance(path);
            logo.SetAbsolutePosition(_PositionX, _PositionY);
            logo.ScalePercent(_PercentScaleX, _PercentScaleY);
            
            return logo;

        }
        
        
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using iTextSharp.text;

namespace InfojectiveReports
{
    class TextSimple : ITextItem
    {
        
        
        

        public TextSimple(XElement reportItem)
        {
            _XMLConfig = reportItem.ToString();
            FillfromXML(reportItem);
        }
        
        private string _text;
        public string Text
        {
            get
            {
                return _text;
            }
            set
            {
                _text = value;
            }
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
        
        private string _name = "Default Text Report Item Name";
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
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
        
        private string _XMLConfig;
        public string XMLConfig
        {
            get
            {
                return _XMLConfig;
            }
            set
            {
                _XMLConfig = value;
            }
        }

        private float _SpacingAfter = 5;
        public float SpacingAfter
        {
            get { return _SpacingAfter; }
            set { _SpacingAfter = value; }
        }


        private float _SpacingBefore = 5;
        public float SpacingBefore
        {
            get { return _SpacingBefore; }
            set { _SpacingBefore = value; }
        }

        private Font.FontFamily _FontFamily = Font.FontFamily.TIMES_ROMAN;
        public Font.FontFamily FontFamily
        {
            get { return _FontFamily; }
            set { _FontFamily = value; }
        }

        private float _FontSize = 10;
        public float FontSize
        {
            get { return _FontSize; }
            set { _FontSize = value; }
        }

        private BaseColor _FontColor = BaseColor.BLACK;
        public BaseColor FontColor
        {
            get { return _FontColor; }
            set { _FontColor = value; }
        }

        private int _FontStyle = Font.NORMAL;
        public int FontStyle
        {
            get { return _FontStyle; }
            set { _FontStyle = value; }
        }
        
        
        public void FillfromXML(XElement reportItem)
        {
            if (!reportItem.HasElements)
            {_text = reportItem.Value; }
            else
            { throw new Exception("Text elements may not contain other elements"); }

            if (reportItem.Attribute("SpacingAfter") != null)
            {
                _SpacingAfter = Convert.ToSingle(reportItem.Attribute("SpacingAfter").Value) ;
            }

            if (reportItem.Attribute("SpacingBefore") != null)
            {
                _SpacingBefore = Convert.ToSingle(reportItem.Attribute("SpacingBefore").Value) ;
            }

            #region FontStyle
            if (reportItem.Attribute("FontStyle") != null)
            {
                switch (reportItem.Attribute("FontStyle").Value)
                {
                    case "normal":
                        _FontStyle = Font.NORMAL;
                        break;
                    case "italic":
                        _FontStyle = Font.ITALIC;
                        break;
                    case "bold":
                        _FontStyle = Font.BOLD;
                        break;
                }
            }
            #endregion

            #region FontColor
            if (reportItem.Attribute("FontColor") != null)
            {
                switch (reportItem.Attribute("FontColor").Value)
                {
                    case "black":
                        _FontColor = BaseColor.BLACK;
                        break;
                    case "green":
                        _FontColor = BaseColor.GREEN;
                        break;
                    case "blue":
                        _FontColor = BaseColor.BLUE;
                        break;
                    case "red":
                        _FontColor = BaseColor.RED;
                        break;
                    case "grey":
                        _FontColor = BaseColor.GRAY;
                        break;
                }
            }
            #endregion

            #region FontSize
            if (reportItem.Attribute("FontSize") != null)
            {
                _FontSize = Convert.ToSingle(reportItem.Attribute("FontSize").Value);
            }
            #endregion

            #region FontFamily
            if (reportItem.Attribute("FontFamily") != null)
            {
                switch (reportItem.Attribute("FontFamily").Value)
                {
                    case "times_roman":
                        _FontFamily = Font.FontFamily.TIMES_ROMAN;
                        break;
                    case "helvetica":
                        _FontFamily = Font.FontFamily.HELVETICA;
                        break;
                    case "courier":
                        _FontFamily = Font.FontFamily.COURIER;
                        break;
                }
            }
            #endregion


        }
        
        

        public IElement GetPDFElement()
        {
            Paragraph newPara = new Paragraph(_text, new Font(_FontFamily, _FontSize, _FontStyle, _FontColor));
            newPara.SpacingAfter = _SpacingAfter;
            newPara.SpacingBefore = _SpacingBefore;
            return newPara;
        }
    }
}

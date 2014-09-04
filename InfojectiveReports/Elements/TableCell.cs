using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using iTextSharp.text.pdf;
using System.Xml.Linq;
using iTextSharp.text;

namespace InfojectiveReports
{
    public class TableCell : ITableCell
    {


        #region Ctor
        public TableCell(XElement columnConfig)
        {
            _xmlConfig = columnConfig;
            FillfromXML();
        } 
        #endregion

        #region Props
        private string _name;
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

        private float _Padding = 3;
        public float Padding
        {
            get { return _Padding; }
            set { _Padding = value; }
        }

        private float _PaddingTop;
        public float PaddingTop
        {
            get { return _PaddingTop; }
            set { _PaddingTop = value; }
        }

        private float _PaddingBottom;
        public float PaddingBottom
        {
            get { return _PaddingBottom; }
            set { _PaddingBottom = value; }
        }

        private float _PaddingLeft;
        public float PaddingLeft
        {
            get { return _PaddingLeft; }
            set { _PaddingLeft = value; }
        }

        private float _PaddingRight;
        public float PaddingRight
        {
            get { return _PaddingRight; }
            set { _PaddingRight = value; }
        }

        private int _Align = Element.ALIGN_LEFT;
        public int Align
        {
            get { return _Align; }
            set { _Align = value; }
        }

        private int _VerticalAlign = PdfPCell.ALIGN_BOTTOM;
        public int VerticalAlign
        {
            get { return _VerticalAlign; }
            set { _VerticalAlign =  value; }
        }
        
        private float _width;
        public string Width
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

        private int _colSpan = 1;
        public int ColSpan
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

        private int _Border = Rectangle.BOX;
        public int Border
        {
            get { return _Border; }
            set { _Border = value; }
        }

        private int _BorderWidth = 1;
        public int BorderWidth
        {
            get { return _BorderWidth; }
            set { _BorderWidth = value; }
        }

        private float _BorderWidthTop;
        public float BorderWidthTop
        {
            get { return _BorderWidthTop; }
            set { _BorderWidthTop = value; }
        }

        private float _BorderWidthBottom;
        public float BorderWidthBottom
        {
            get { return _BorderWidthBottom; }
            set { _BorderWidthBottom = value; }
        }

        private float _BorderWidthLeft;
        public float BorderWidthLeft
        {
            get { return _BorderWidthLeft; }
            set { _BorderWidthLeft = value; }
        }

        private float _BorderWidthRight;
        public float BorderWidthRight
        {
            get { return _BorderWidthRight; }
            set { _BorderWidthRight = value; }
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

        private BaseColor _CellColor = BaseColor.WHITE;

        public BaseColor CellColor
        {
            get { return _CellColor; }
            set { _CellColor = value; }
        }
        
        
        private int _FontStyle = Font.NORMAL;
        public int FontStyle
        {
            get { return _FontStyle; }
            set { _FontStyle = value; }
        }

        private string _Binding;
        public string Binding
        {
            get
            {
                return _Binding;
            }
            set
            {
                _Binding = value;
            }
        }

        private string _Header = "";

        public string Header
        {
            get { return _Header; }
            set { _Header = value; }
        }
        

        private XElement _xmlConfig;
        public XElement XMLConfig
        {
            get
            {
                return _xmlConfig;
            }
            set
            {
                _xmlConfig = value;
            }
        } 
        #endregion

        public PdfPCell GetCell(string value)
        {
                        
            PdfPCell newCell = new PdfPCell(new Phrase(value, new Font(_FontFamily,_FontSize,_FontStyle,_FontColor)));
            newCell.Colspan = _colSpan;
            newCell.Padding = _Padding;
            newCell.BackgroundColor = _CellColor;
            newCell.HorizontalAlignment = _Align;
            newCell.Border = _Border;
            newCell.VerticalAlignment = _VerticalAlign;
            return newCell;
        }

        private void FillfromXML()
        {

            #region FontStyle
            if (_xmlConfig.Attribute("FontStyle") != null)
            {
                switch (_xmlConfig.Attribute("FontStyle").Value)
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

            #region CellColor
            if (_xmlConfig.Attribute("CellColor") != null)
            {
                switch (_xmlConfig.Attribute("CellColor").Value)
                { 
                    case "lightGrey":
                        _CellColor = BaseColor.LIGHT_GRAY;
                        break;
                    case "white":
                        _CellColor = BaseColor.WHITE;
                        break;
                    case "darkGrey":
                        _CellColor = BaseColor.DARK_GRAY;
                        break;
                    case "red":
                        _CellColor = BaseColor.RED;
                        break;

                }
            }
            #endregion

            #region FontColor
            if (_xmlConfig.Attribute("FontColor") != null)
            {
                switch (_xmlConfig.Attribute("FontColor").Value)
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
            if (_xmlConfig.Attribute("FontSize") != null)
            {
                _FontSize = Convert.ToSingle(_xmlConfig.Attribute("FontSize").Value);
            } 
            #endregion

            #region FontFamily
            if (_xmlConfig.Attribute("FontFamily") != null)
            {
                switch (_xmlConfig.Attribute("FontFamily").Value)
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

            #region colSpan
            if (_xmlConfig.Attribute("colSpan") != null)
            {
                _colSpan = Convert.ToInt16(_xmlConfig.Attribute("colSpan").Value);
            } 
            #endregion

            #region BorderWidth
            if (_xmlConfig.Attribute("borderWidth") != null)
            {
                _BorderWidth = Convert.ToInt16(_xmlConfig.Attribute("borderWidth").Value);
            } 
            #endregion

            #region Padding
            if (_xmlConfig.Attribute("Padding") != null)
            {
                _Padding = Convert.ToSingle(_xmlConfig.Attribute("Padding").Value);
            } 
            #endregion

            #region Binding
            if (_xmlConfig.Attribute("binding") != null)
            {
                _Binding = _xmlConfig.Attribute("binding").Value;
            } 
            #endregion

            #region Align
            if (_xmlConfig.Attribute("Align") != null)
            {
                switch (_xmlConfig.Attribute("Align").Value)
                {
                    case "left":
                        _Align = Element.ALIGN_LEFT;
                        break;
                    case "center":
                        _Align = Element.ALIGN_CENTER;
                        break;
                    case "right":
                        _Align = Element.ALIGN_RIGHT;
                        break;
                }
            } 
            #endregion

            #region VerticalAlign
            if (_xmlConfig.Attribute("VerticalAlign") != null)
            {
                switch (_xmlConfig.Attribute("VerticalAlign").Value)
                {
                    case "top":
                        _VerticalAlign = Element.ALIGN_TOP;
                        break;
                    case "middle":
                        _VerticalAlign = Element.ALIGN_MIDDLE;
                        break;
                    case "bottom":
                        _VerticalAlign = Element.ALIGN_BOTTOM;
                        break;

                }
            } 
            #endregion

            #region Border
            if (_xmlConfig.Attribute("Border") != null)
            {
                switch (_xmlConfig.Attribute("Border").Value)
                {
                    case "left":
                        _Border = Rectangle.LEFT_BORDER;
                        break;
                    case "right":
                        _Border = Rectangle.RIGHT_BORDER;
                        break;
                    case "top":
                        _Border = Rectangle.TOP_BORDER;
                        break;
                    case "bottom":
                        _Border = Rectangle.BOTTOM_BORDER;
                        break;
                    case "box":
                        _Border = Rectangle.BOX;
                        break;
                    case "none":
                        _Border = Rectangle.NO_BORDER;
                        break;

                }

            } 
            #endregion

            if (_xmlConfig.Attribute("Header") != null)
            {
                _Header = _xmlConfig.Attribute("Header").Value;
            }
            

        }

    }
}

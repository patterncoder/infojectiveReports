using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using iTextSharp.text.pdf;
using System.Xml.Linq;
using iTextSharp.text;

namespace InfojectiveReports
{
    public interface ITableCell
    {
        string Name { get; set; }
        int ColSpan { get; set; }
        float Padding { get; set; }
        float PaddingTop { get; set; }
        float PaddingBottom { get; set; }
        float PaddingLeft { get; set; }
        float PaddingRight { get; set; }
        int Align { get; set; }
        int VerticalAlign { get; set; }
        int Border { get; set; }
        int BorderWidth { get; set; }
        float BorderWidthTop { get; set; }
        float BorderWidthBottom { get; set; }
        float BorderWidthLeft { get; set; }
        float BorderWidthRight { get; set; }
        string Binding { get; set; }
        string Header { get; set; }
        Font.FontFamily FontFamily { get; set; }
        float FontSize { get; set; }
        int FontStyle { get; set; }
        BaseColor FontColor { get; set; }

        XElement XMLConfig { get; set; }
        PdfPCell  GetCell(string value);

        
    }
}

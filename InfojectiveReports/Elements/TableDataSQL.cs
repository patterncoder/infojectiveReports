using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System.Data;



namespace InfojectiveReports
{
    public class TableDataSQL : ITableData
    {
        
        
        private DataTable _tableData;



        public TableDataSQL(XElement reportItem, string conn, List<IParameter> parameters)
        {
            _parameters = parameters;
            _XMLConfig = reportItem.ToString();
            _Connection = conn;
            FillfromXML(reportItem);
        }

        private List<IParameter> _parameters;

        public List<IParameter> Parameters
        {
            get { return _parameters; }
            set { _parameters = value; }
        }
        

        private float _SpacingBefore = 0;

        public float SpacingBefore
        {
            get { return _SpacingBefore; }
            set { _SpacingBefore = value; }
        }

        private float _SpacingAfter = 0;

        public float SpacingAfter
        {
            get { return _SpacingAfter; }
            set { _SpacingAfter = value; }
        }
        
        
        private int _TableAlign = Element.ALIGN_CENTER;
        public int TableAlign
        {
            get { return _TableAlign; }
            set { _TableAlign = value; }
        }
        
        private float _TableWidth = 540 ;
        public float TableWidth
        {
            get { return _TableWidth; }
            set { _TableWidth = value; }
        }

        private string _TagName;
        public string TagName
        {
            get
            {
                return _TagName;
            }
            set
            {
                _TagName = value;
            }
        }

        private string _Name = "Default TableDataSQL Report Item Name";
        public string Name
        {
            get
            {
                return _Name;
            }
            set
            {
               _Name = value;
            }
        }

        private bool _Visible;
        public bool Visible
        {
            get
            {
                return _Visible;
            }
            set
            {
                _Visible = value;
            }
        }

        private string _Connection;
        public string Connection
        {
            get
            {
                return _Connection;
            }
            set
            {
                _Connection = value;
            }
        }

        private string _DataSource;
        public string DataSource
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

        private int _ColumnCount;
        public int ColumnCount
        {
            get
            {
                return _ColumnCount;
            }
            set
            {
               _ColumnCount = value;
            }
        }

        private int[] _columnWidths;
        public int[] ColumnWidths
        {
            get { return _columnWidths; }
            set { _columnWidths = value; }
        }

        private List<ITableRow> _Rows;
        public List<ITableRow> Rows
        {
            get
            {
                return _Rows;
            }
            set
            {
                _Rows = value;
            }
        }

        //private List<ITableCell> _Columns;
        //public List<ITableCell> Cells
        //{ get { return _Columns; } set { _Columns = value; } }

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
       


        private void FillfromXML(XElement itemData)
        {
            // Fill 'this' from xml element
            #region TableSettings
            XNamespace ns = itemData.Name.Namespace;
            if (itemData.Attribute("SpacingBefore") != null)
            {
                _SpacingBefore = Convert.ToSingle(itemData.Attribute("SpacingBefore").Value);
            }

            if (itemData.Attribute("SpacingAfter") != null)
            {
                _SpacingAfter = Convert.ToSingle(itemData.Attribute("SpacingAfter").Value);
            }

            _ColumnCount = Convert.ToInt16(itemData.Attribute("columnCount").Value);
            if (itemData.Attribute("tableWidth") != null)
            { 
                _TableWidth = Convert.ToSingle(itemData.Attribute("tableWidth").Value);
            }
            
            _TagName = itemData.Name.ToString();
            _DataSource = itemData.Attribute("DataSource").Value;
            //_Name = itemData.Attribute("name").Value;
            _tableData = new DBConnHelper(_Connection,_parameters).GetDataTableSQL(_DataSource);
            _columnWidths = StringArray.GetWidths(itemData.Attribute("columnWidths").Value);
            if (itemData.Attribute("tableAlign") != null)
            {
                switch (itemData.Attribute("tableAlign").Value.ToString())
                {
                    case "left":
                        _TableAlign = Element.ALIGN_LEFT;
                        break;
                    case "right":
                        _TableAlign = Element.ALIGN_RIGHT;
                        break;
                    case "center":
                        _TableAlign = Element.ALIGN_CENTER;
                        break;
                }
            }
            #endregion 
            
            List<ITableRow> _NewRows = new List<ITableRow>();
           
            foreach (XElement row in itemData.Elements(ns + "Row"))
            {
                ITableRow newRow = new TableRow();
                List<ITableCell> _Columns = new List<ITableCell>();

                foreach (XElement column in row.Elements(ns + "Cell"))
                {
                    ITableCell newCell = new TableCell(column);
                    _Columns.Add(newCell);
                    
                }
                newRow.RowCells = _Columns;
                _NewRows.Add(newRow);
            }
            _Rows = _NewRows;
        }

       

        public IElement GetPDFElement()
        {
            PdfPTable myTable = new PdfPTable(_ColumnCount);
            
            myTable.LockedWidth = true;
            myTable.TotalWidth = _TableWidth;
            myTable.HorizontalAlignment = _TableAlign;
            myTable.SetWidths(_columnWidths);
            myTable.SpacingBefore = _SpacingBefore;
            myTable.SpacingAfter = _SpacingAfter;
            
            // add column headers here

            foreach (ITableRow row in this._Rows)
            {
                foreach (ITableCell cell in row.RowCells)
                {
                    if (cell.Header.Length>0)
                    {
                        PdfPCell newcell = cell.GetCell(cell.Header);
                        newcell.BackgroundColor = BaseColor.LIGHT_GRAY;
                        newcell.Border = Rectangle.BOX;
                        foreach (Chunk chunk in newcell.Chunks)
                        {
                            chunk.Font.SetStyle(Font.BOLD);
                        }
                        
                        myTable.AddCell(newcell);
                    }
                }
            }

            foreach (DataRow datarow in _tableData.Rows)
            {
                foreach (ITableRow row in this._Rows)
                {
                    foreach (ITableCell cell in row.RowCells)
                    {
                        if (cell.Binding.Equals(string.Empty))
                        {
                            PdfPCell newcell = cell.GetCell("");
                            myTable.AddCell(newcell);
                        }
                        if (!cell.Binding.Contains("{")) 
                        {
                            PdfPCell newcell = cell.GetCell(cell.Binding);
                            myTable.AddCell(newcell);
                        }
                        else
                        {
                            BindingParser myParser = new BindingParser(cell.Binding);
                            
                            PdfPCell newcell = cell.GetCell(myParser.InjectData(datarow[myParser.GetField()].ToString()));
                            
                            myTable.AddCell(newcell);
                        }

                        
                    }
                }
                
            }
           
            

            
            return myTable; ;
        }


      
    }
}

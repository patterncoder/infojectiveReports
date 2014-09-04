using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using iTextSharp.text;

namespace InfojectiveReports
{
    public class Report : IReport
    {

        private string _name;
        private string _title;
        private string _description;
        private string _connection;
        private float _marginTop;
        private float _marginBottom;
        private float _marginLeft;
        private float _marginRight;
        private Rectangle _pageSize;
        private List<IReportItem> _reportItems;

        
        
        
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

        public string Title
        {
            get
            {
                return _title;
            }
            set
            {
                _title = value;
            }
        }

        public string Description
        {
            get
            {
                return _description;
            }
            set
            {
                _description = value;
            }
        }

        public float MarginTop
        {
            get
            {
                return _marginTop;  
            }
            set
            {
                _marginTop = value;
            }
        }

        public float MarginBottom
        {
            get
            {
                return _marginBottom;
            }
            set
            {
                _marginBottom = value;
            }
        }

        public float MarginLeft
        {
            get
            {
                return _marginLeft;
            }
            set
            {
                _marginLeft = value;
            }
        }

        public float MarginRight
        {
            get
            {
                return _marginRight;
            }
            set
            {
                _marginRight = value;
            }
        }

        public Rectangle PageSize
        {
            get
            {
                return _pageSize;
            }
            set
            {
                _pageSize = value;
            }
        }

        public string Connection
        {
            get
            {
                return _connection;
            }
            set
            {
                _connection = value;
            }
        }

        private string _Orientation;
        public string Orientation
        {
            get { return _Orientation; }
            set { _Orientation = value; }
        }

        private string _PageEvent = "TwoColumnHeaderFooter";

        public string PageEvent
        {
            get { return _PageEvent; }
            set { _PageEvent = value; }
        }
        
    

public List<IReportItem>  ReportItems
{
	  get 
	{ 
		return _reportItems; 
	}
	  set 
	{ 
		_reportItems = value; 
	}
}
}
}

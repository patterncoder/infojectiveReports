using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using InfojectiveReports;
using System.IO;
using System.Diagnostics;
using System.Configuration;

namespace Infojective.Reports.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<IParameter> _listParams;
        string _reportsFolder;
        
        public MainWindow()
        {
            InitializeComponent();
            _reportsFolder = ConfigurationManager.AppSettings["ReportsFolder"];
            DirectoryInfo diFiles = new DirectoryInfo(_reportsFolder);
            
            listBox1.ItemsSource = diFiles.GetFiles("*.xml");
            _listParams = new List<IParameter>();
            
            //lstFiles.DataSource = diFiles.GetFiles("*.xml");
            //lstFiles.DataBind();
        }

        private void RunReport(object sender, RoutedEventArgs e)
        {
            try
            {
                string path = _reportsFolder + listBox1.SelectedValue;
                //Dictionary<string, string> myDic = GetDictionary();
                Dictionary<string, string> myDic = new Dictionary<string, string>();
                //foreach (IParameter listParam in listParams)
                //{
                //    myDic.Add(listParam.Name, listParam.Value);
                //}
                //myDic.Add("@EndDateDate", "200");
                //IReport report = new ReportPreProcessor().PreProcess(path,myDic);
                
                IReportController reportController = new ReportController();
                IReport myReport = new ReportPreProcessor().PreProcess(path, _listParams);
                byte[] content = reportController.CreatePDF(myReport, _listParams).ToArray();
                //IReport myReport = new ReportPreProcessor().PreProcess(path, _listParams);
                
                //byte[] content = reportController.CreatePDF(myReport, _listParams).ToArray();
                string fileLocat = Environment.GetEnvironmentVariable("TEMP");
                fileLocat += "SaveMe.pdf";
                using (FileStream fs = File.Create(fileLocat))
                {
                    fs.Write(content, 0, (int)content.Length);
                }

                Process.Start("AcroRd32.exe", fileLocat);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Console.Write(ex.InnerException);
                return;
            }
            

            //string batchFileName = "temp_pdflauncher.bat";

            //using (StreamWriter sw = new StreamWriter(batchFileName, false))

            //    {

            //        sw.WriteLine("call \"" + @"c:\aTestFile5.pdf" + "\"");

            //    }

            //Process.Start("cmd", "/c " + batchFileName);
        }

        private Dictionary<string, string> GetDictionary()
        {
            Dictionary<string,string> myDic = new Dictionary<string,string>();
            foreach (IParameter param in _listParams)
            {
                
                
                myDic.Add(param.Name, param.Value);
            }
            return myDic;
        }

        private void listBox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string path = ConfigurationManager.AppSettings["ReportsFolder"];
            path += listBox1.SelectedValue.ToString();
            _listParams = ReportParameters.GetParameters(path);
            itemsControl1.ItemsSource = _listParams;
            
            
        }

       
    }
}

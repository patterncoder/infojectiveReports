using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace InfojectiveReports
{
    public static class ReportParameters 
    {
        

        
        public static List<IParameter> GetParameters(string reportTemplatePath)
        {
            XDocument doc = XDocument.Load(reportTemplatePath);
            XNamespace ns = doc.Root.Name.Namespace;
            //Console.Write(doc.ToString());
            //Console.ReadKey();
            //Console.WriteLine(doc.Root.Element(ns + "Parameters").Element(ns + "Parameter").Attribute("Name").Value);
            //Console.ReadKey();
            List<IParameter> newList = new List<IParameter>();
            if (doc.Root.Element(ns + "Parameters") != null)
            {
                foreach (XElement xparam in doc.Root.Element(ns + "Parameters").Descendants()) // doc.Elements("Parameter") doc.Root.Element("Parameters").Descendants()
                {
                    IParameter newParam = new Parameter();
                    newParam.Name = xparam.Attribute("Name").Value;
                    newParam.ParameterType = xparam.Attribute("ParameterType").Value;
                    newParam.ValidationText = xparam.Attribute("ValidationText").Value;
                    //Console.WriteLine(newParam.Name);
                    newList.Add(newParam);

                }
            }


            return newList;
        }
    }
}

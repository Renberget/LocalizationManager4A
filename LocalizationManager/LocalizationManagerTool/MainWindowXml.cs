using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace LocalizationManagerTool
{
    public partial class MainWindow
    {
        private void ImportXml(string filename)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(filename);

            if (!doc.HasChildNodes)
                return;

            foreach (XmlNode nameNode in doc.ChildNodes[0])
            {
                if (nameNode.Attributes.Count == 0)
                    continue;

                foreach (XmlNode languageNode in nameNode.ChildNodes)
                {
                    //Trace.WriteLine(languageNode.Name);
                    //dataTable.Rows.Add(parser.ReadFields());
                }
            }
        }

        private void ExportXml(string filename)
        {
            //XmlDocument doc = new XmlDocument();

            //doc.AppendChild();
            //XmlNode node;

            //doc.Save(filename);
        }
    }
}

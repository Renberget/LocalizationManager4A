using System.Data;
using System.Diagnostics;
using System.IO;
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
            if (!doc.ChildNodes[0].HasChildNodes)
                return;

            XmlNode rootNode = doc.ChildNodes[0];

            dataTable.Columns.Add("id", typeof(string));

            for (int i = 0; i < rootNode.ChildNodes.Count; ++i)
            {
                XmlNode nameNode = rootNode.ChildNodes[i];
                if (nameNode.Attributes.Count == 0)
                    continue;

                if (i == 0)
                {
                    //First Row
                    foreach (XmlNode languageNode in nameNode.ChildNodes)
                    {
                        dataTable.Columns.Add(languageNode.Name, typeof(string));
                    }
                }

                string[] row = new string[dataTable.Columns.Count];
                row[0] = nameNode.Attributes[0].Value;
                for (int j = 0; j < nameNode.ChildNodes.Count; ++j)
                {
                    XmlNode languageNode = nameNode.ChildNodes[j];
                    row[1 + j] = languageNode.InnerText;
                }
                dataTable.Rows.Add(row);
            }
        }

        private void ExportXml(string filename)
        {
            XmlWriter writer = XmlWriter.Create(filename, new XmlWriterSettings { Indent = true });
            writer.WriteStartDocument();

            writer.WriteStartElement("languages");

            foreach (DataRow row in dataTable.Rows)
            {
                writer.WriteStartElement("id");
                writer.WriteAttributeString("value", (string)row.ItemArray[0]);

                for (int i = 1; i < dataTable.Columns.Count; ++i)
                {
                    writer.WriteStartElement(dataTable.Columns[i].ColumnName);
                    writer.WriteString((string)row.ItemArray[i]);
                    writer.WriteEndElement();
                }
                writer.WriteEndElement();
            }

            writer.WriteEndDocument();
            writer.Close();
        }
    }
}

using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalizationManagerTool
{
    public partial class MainWindow
    {
        private void ImportCsv(string filename)
        {
            using (TextFieldParser parser = new TextFieldParser(filename))
            {
                parser.SetDelimiters(",", ";");
                while (!parser.EndOfData)
                {
                    dataTable.Rows.Add(parser.ReadFields());
                }
            }
        }

        private void ExportCsv(string filename)
        {
            StreamWriter stream = new StreamWriter(filename, false);
            foreach (DataRow row in dataTable.Rows)
            {
                stream.WriteLine(String.Join(';', row.ItemArray));
            }
            stream.Close();
        }
    }
}

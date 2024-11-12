using Microsoft.VisualBasic.FileIO;
using System.Data;
using System.IO;

namespace LocalizationManagerTool
{
    public partial class MainWindow
    {
        private void ImportCsv(string filename)
        {
            TextFieldParser parser = new TextFieldParser(filename);
            parser.SetDelimiters(",", ";");

            if (parser.EndOfData)
                return;

            foreach (string column in parser.ReadFields())
            {
                dataTable.Columns.Add(column, typeof(string));
            }
            
            while (!parser.EndOfData)
            {
                dataTable.Rows.Add(parser.ReadFields());
            }
        }

        private void ExportCsv(string filename)
        {
            StreamWriter stream = new StreamWriter(filename, false);
            string columnNames = "";
            foreach (DataColumn column in dataTable.Columns)
            {
                columnNames += column.ColumnName + ';';
            }
            columnNames = columnNames.Remove(columnNames.Length - 1, 1);
            stream.WriteLine(columnNames);

            foreach (DataRow row in dataTable.Rows)
            {
                stream.WriteLine(String.Join(';', row.ItemArray));
            }
            stream.Close();
        }
    }
}

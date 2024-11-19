using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;

namespace LocalizationManagerTool
{
    public partial class MainWindow
    {
        private void ExportCs(string filename)
        {
            StringBuilder content = new StringBuilder();

            content.Append("public class Localization");
            content.AppendLine();
            content.Append('{');
            content.AppendLine();

            if (dataGrid.ItemsSource is DataView dataView)
            {
                foreach (DataRowView rowView in dataView)
                {
                    for (int i = 1; i < dataView.Table.Columns.Count; i++)
                    {
                        content.Append("    string " + dataView.Table.Columns[i] + rowView[0] + " = \"" + rowView[i] + "\";");
                        content.AppendLine();
                    }
                    content.AppendLine();
                }

                foreach (DataRowView rowView in dataView)
                {
                    for (int i = 1; i < dataView.Table.Columns.Count; i++)
                    {
                        content.Append("    public string Get" + dataView.Table.Columns[i].ToString().ToUpper() + rowView[0] + "()");
                        content.AppendLine();
                        content.Append("    {");
                        content.AppendLine();
                        content.Append("        return " + dataView.Table.Columns[i] + rowView[0] + ";");
                        content.AppendLine();
                        content.Append("    }");
                        content.AppendLine();
                        content.AppendLine();
                    }
                }
            }

            content.Append('}');

            File.WriteAllText(filename, content.ToString());
        }
    }
}

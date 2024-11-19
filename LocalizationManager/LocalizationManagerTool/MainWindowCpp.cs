using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;

namespace LocalizationManagerTool
{
    public partial class MainWindow
    {
        private void ExportCpp(string filename)
        {
            StringBuilder contentCPPFile = new StringBuilder();
            StringBuilder contentHFile = new StringBuilder();

            contentHFile.Append("#pragma once");
            contentHFile.AppendLine();
            contentHFile.Append("#include <string>");
            contentHFile.AppendLine();
            contentHFile.AppendLine();
            contentHFile.Append("class Localization");
            contentHFile.AppendLine();
            contentHFile.Append('{');
            contentHFile.AppendLine();

            contentCPPFile.Append("#include \"Localization.h\"");
            contentCPPFile.AppendLine();
            contentCPPFile.Append("#include <string>");
            contentCPPFile.AppendLine();
            contentCPPFile.AppendLine();

            if (dataGrid.ItemsSource is DataView dataView)
            {
                contentHFile.Append("    private:");
                contentHFile.AppendLine();

                foreach (DataRowView rowView in dataView)
                {
                    for (int i = 1; i < dataView.Table.Columns.Count; i++)
                    {
                        contentHFile.Append("        std::string " + dataView.Table.Columns[i] + rowView[0] + " = \"" + rowView[i] + "\";");
                        contentHFile.AppendLine();
                    }
                    contentHFile.AppendLine();
                }

                contentHFile.Append("    public:");
                contentHFile.AppendLine();

                foreach (DataRowView rowView in dataView)
                {
                    for (int i = 1; i < dataView.Table.Columns.Count; i++)
                    {
                        contentHFile.Append("        std::string Get" + dataView.Table.Columns[i].ToString().ToUpper() + rowView[0] + "();");
                        contentHFile.AppendLine();

                        contentCPPFile.Append("std::string Localization::Get" + dataView.Table.Columns[i].ToString().ToUpper() + rowView[0] + "()");
                        contentCPPFile.AppendLine();
                        contentCPPFile.Append("{");
                        contentCPPFile.AppendLine();
                        contentCPPFile.Append("    return " + dataView.Table.Columns[i] + rowView[0] + ";");
                        contentCPPFile.AppendLine();
                        contentCPPFile.Append("}");
                        contentCPPFile.AppendLine();
                        contentCPPFile.AppendLine();
                    }

                    contentHFile.AppendLine();
                }
            }

            contentHFile.Append("};");

            File.WriteAllText(filename + ".cpp", contentCPPFile.ToString());
            File.WriteAllText(filename + ".h", contentHFile.ToString());
        }
    }
}

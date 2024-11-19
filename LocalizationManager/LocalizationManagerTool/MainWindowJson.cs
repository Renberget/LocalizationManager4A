using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Data;
using System.IO;

namespace LocalizationManagerTool
{
    public partial class MainWindow
    {
        private void ImportJson(string filename)
        {
            StreamReader streamReader = new StreamReader(filename);
            JObject root = JObject.Parse(streamReader.ReadToEnd());

            dataTable.Columns.Add("id", typeof(string));

            bool checkFirstRow = true;
            foreach (KeyValuePair<string, JToken?> pair in root)
            {
                if (checkFirstRow)
                {
                    checkFirstRow = false;
                    foreach (JProperty property in pair.Value.Children<JProperty>())
                    {
                        dataTable.Columns.Add(property.Name, typeof(string));
                    }
                }

                string[] row = new string[dataTable.Columns.Count];
                row[0] = pair.Key;
                int i = 1;
                foreach (JProperty property in pair.Value.Children<JProperty>())
                {
                    row[i++] = property.Children().First().ToString();
                }
                dataTable.Rows.Add(row);
            }
        }

        private void ExportJson(string filename)
        {
            StreamWriter streamWriter = new StreamWriter(filename);
            JsonWriter writer = new JsonTextWriter(streamWriter);
            writer.Formatting = Formatting.Indented;

            writer.WriteStartObject();

            foreach (DataRow row in dataTable.Rows)
            {
                writer.WritePropertyName((string)row.ItemArray[0]);
                writer.WriteStartObject();

                for (int i = 1; i < dataTable.Columns.Count; ++i)
                {
                    writer.WritePropertyName(dataTable.Columns[i].ColumnName);
                    writer.WriteValue((string)row.ItemArray[i]);
                }
                writer.WriteEndObject();
            }
            writer.WriteEnd();
            writer.Close();
        }
    }
}

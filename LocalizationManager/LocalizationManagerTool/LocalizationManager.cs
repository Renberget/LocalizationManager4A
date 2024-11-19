using Microsoft.VisualBasic.FileIO;
using System.Diagnostics;
using System.Xml;
using Newtonsoft.Json;
using System.IO;
using Newtonsoft.Json.Linq;

//Used on the game side to load csv/xml/json
class LocalizationManager
{
    private Dictionary<string, string> dictionary = new Dictionary<string, string>();

    public void LoadFromCsv(string filename, string language)
    {
        TextFieldParser parser = new TextFieldParser(filename);
        parser.SetDelimiters(",", ";");

        if (parser.EndOfData)
            return;

        int languageIndex = Array.IndexOf(parser.ReadFields(), language);
        if (languageIndex == -1)
            return;

        while (!parser.EndOfData)
        {
            string[] fields = parser.ReadFields();
            dictionary.Add(fields[0], fields[languageIndex]);
        }
    }

    public void LoadFromXml(string filename, string language)
    {
        XmlDocument doc = new XmlDocument();
        doc.Load(filename);

        if (!doc.HasChildNodes)
            return;
        if (!doc.ChildNodes[0].HasChildNodes)
            return;

        XmlNode rootNode = doc.ChildNodes[0];
        int languageIndex = -1;

        for (int i = 0; i < rootNode.ChildNodes.Count; ++i)
        {
            XmlNode nameNode = rootNode.ChildNodes[i];
            if (nameNode.Attributes.Count == 0)
                continue;

            if (i == 0)
            {
                for (int j = 0; j < nameNode.ChildNodes.Count; ++j)
                {
                    if (nameNode.ChildNodes[j].Name == language)
                    {
                        languageIndex = j;
                        break;
                    }
                }
                
                if (languageIndex == -1)
                    return;
            }

            for (int j = 0; j < nameNode.ChildNodes.Count; ++j)
            {
                if (language == nameNode.ChildNodes[j].Name)
                {
                    dictionary.Add(nameNode.Attributes[0].Value, nameNode.ChildNodes[j].InnerText);
                    break;
                }
            }
        }
    }

    public void LoadFromJson(string filename, string language)
    {
        StreamReader streamReader = new StreamReader(filename);
        JObject root = JObject.Parse(streamReader.ReadToEnd());
        foreach (KeyValuePair<string, JToken?> pair in root)
        {
            dictionary.Add(pair.Key, pair.Value[language].ToString());
        }
    }

    public string? Get(string key)
    {
        return dictionary[key];
    }
}
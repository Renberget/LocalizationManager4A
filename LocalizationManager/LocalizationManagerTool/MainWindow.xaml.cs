using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace LocalizationManagerTool
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DataTable dataTable = new DataTable();

        public MainWindow()
        {
            InitializeComponent();
            dataTable.Columns.Add("id");
            dataTable.Columns.Add("en");
            dataTable.Columns.Add("fr");
            dataTable.Columns.Add("es");
            dataTable.Columns.Add("ja");
            dataGrid.ItemsSource = dataTable.DefaultView;
        }

        private void Button_New(object sender, RoutedEventArgs e)
        {
            
        }

        private void Button_Open(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "CSV|*.csv|JSON|*.json|XML|*.xml";

            if (dialog.ShowDialog() == true)
            {
                string filename = dialog.FileName;
                string extension = Path.GetExtension(filename);
                switch (extension)
                {
                    case ".csv":
                        ImportCsv(filename);
                        break;
                    case ".json":
                        ImportJson(filename);
                        break;
                    case ".xml":
                        ImportXml(filename);
                        break;
                }
            }
        }

        private void Button_Export(object sender, RoutedEventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "CSV|*.csv|JSON|*.json|XML|*.xml|C# Singleton|*.cs|C++ Singleton|*.hpp";

            if (dialog.ShowDialog() == true)
            {
                string filename = dialog.FileName;
                string extension = Path.GetExtension(filename);
                switch (extension)
                {
                    case ".csv":
                        ExportCsv(filename);
                        break;
                    case ".json":
                        ExportJson(filename);
                        break;
                    case ".xml":
                        ExportCsv(filename);
                        break;
                    case ".cs":
                        ExportCs(filename);
                        break;
                    case ".hpp":
                        ExportCpp(filename);
                        break;
                }
            }
        }
    }
}
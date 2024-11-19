using Microsoft.Win32;
using System.Data;
using System.IO;
using System.Windows;

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
            dataGrid.ItemsSource = dataTable.DefaultView;
            dataTable.Columns.Add("id", typeof(string));
        }

        private void Button_New(object sender, RoutedEventArgs e)
        {
            dataTable = new DataTable();
            dataTable.Columns.Add("id", typeof(string));
            dataGrid.ItemsSource = dataTable.DefaultView;
        }

        private void Button_Open(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "CSV|*.csv|JSON|*.json|XML|*.xml";

            if (dialog.ShowDialog() == true)
            {
                string filename = dialog.FileName;
                string extension = Path.GetExtension(filename);

                dataTable = new DataTable();
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

        private void AddColumn_Click(object sender, RoutedEventArgs e)
        {
            AddColumnDialog addColumnDialog = new AddColumnDialog(AddColumn);
            addColumnDialog.Owner = this;
            addColumnDialog.ShowDialog();
        }

        private void AddColumn(string name)
        {
            dataTable = dataTable.Clone();
            dataTable.Columns.Add(name, typeof(string));
            dataGrid.ItemsSource = dataTable.DefaultView;
        }

        private void Button_ExportCsv(object sender, RoutedEventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "CSV|*.csv";

            if (dialog.ShowDialog() == true)
                ExportCsv(dialog.FileName);
        }

        private void Button_ExportJson(object sender, RoutedEventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "JSON|*.json";

            if (dialog.ShowDialog() == true)
                ExportJson(dialog.FileName);
        }

        private void Button_ExportXml(object sender, RoutedEventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "XML|*.xml";

            if (dialog.ShowDialog() == true)
                ExportXml(dialog.FileName);
        }

        private void Button_ExportCs(object sender, RoutedEventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "C# Singleton|*.cs";

            if (dialog.ShowDialog() == true)
                ExportCs(dialog.FileName);
        }

        private void Button_ExportCpp(object sender, RoutedEventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "C++ Singleton|*.hpp";

            if (dialog.ShowDialog() == true)
                ExportCpp(dialog.FileName);
        }
    }
}
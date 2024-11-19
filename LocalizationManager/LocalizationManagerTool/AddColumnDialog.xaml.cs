using System.Windows;

namespace LocalizationManagerTool
{
    /// <summary>
    /// Interaction logic for AddColumnDialog.xaml
    /// </summary>
    public partial class AddColumnDialog : Window
    {
        public delegate void AddColumn(string name);

        private AddColumn callback;

        public AddColumnDialog(AddColumn addColumnCallback)
        {
            InitializeComponent();
            callback = addColumnCallback;
            NameTextBox.Focus();
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            callback(NameTextBox.Text);
            Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}

using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Expenses
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window
    {
        public int month;
        public int year;
        public List<int> months = new List<int>();
        public List<int> years = new List<int>();
        public double income;
        public double payments;
        public double current;
        List<Record> records;
        List<string> categories = new List<string>();
        public MainWindow()
        {
            DB.Instance.Reload();
            InitializeComponent();
            this.Loaded += MainWindow_Loaded;
            this.Closing += MainWindow_Closing;
            month = 0;
        }

        private async void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            DB.Instance.setCurrentUser(null);
            await DB.Instance.sync();
        }

        private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            combobox1.ItemsSource = DB.Instance.users.Select(x => x.name);
            this.UpdateLayout();
        }


        private void AddUser(object sender, RoutedEventArgs e)
        {
            AddUser addUser = new AddUser();
            addUser.Show();
            this.Close();
        }

        private void SetCurrentUser(object sender = null, RoutedEventArgs e = null)
        {
            Person user = DB.Instance.users[combobox1.SelectedIndex];
            DB.Instance.setCurrentUser(user);
            UserUI ui = new UserUI(user);
            this.Close();
            ui.ShowDialog();
        }
        
    }
}
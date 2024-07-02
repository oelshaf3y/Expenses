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

        public MainWindow()
        {
            DB.Instance.Reload();
            InitializeComponent();
            this.Loaded += MainWindow_Loaded;
            this.Closing += MainWindow_Closing;
        }

        private async void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            DB.Instance.currentUser = null;
            await DB.Instance.sync();
        }

        private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            combobox1.ItemsSource = DB.Instance.users.Select(x => x.name);
            this.UpdateLayout();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AddCategory addCatWindow = new AddCategory();
            addCatWindow.ShowDialog();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            AddSubCat subCatWindow = new AddSubCat();
            subCatWindow.ShowDialog();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            AddRecord addRecordWindow = new AddRecord();
            addRecordWindow.ShowDialog();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            AddUser addUser = new AddUser();
            addUser.Show();
            this.Close();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            DB.Instance.currentUser = DB.Instance.users[combobox1.SelectedIndex];
            List<Record> records = getItems(DB.Instance.categories);
            datagrid.ItemsSource=records;
            double income = records.Where(x => x.index == 1).Select(x => x.value).Sum();
            double payments = records.Where(x => x.index == -1).Select(x => x.value).Sum();
            double current =income - payments;
            currentLabel.Content = "current: "+current.ToString();
            paidLable.Content = "Paid: "+payments.ToString();
            incomeLabel.Content = "Income: " + income.ToString();
        }
        private List<Record> getItems(List<Category> cats)
        {
            List<Record> records = new List<Record>();
            foreach (Category cat in cats)
            {
                if (cat.items.Count > 0)
                {
                    records.AddRange(cat.items.Where(x => x.user.name == DB.Instance.currentUser.name));
                }
                if (cat.children.Count > 0)
                {
                    records.AddRange(getItems(cat.children));
                }
            }
            return (records);
        }
    }
}
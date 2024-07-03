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
            DB.Instance.currentUser = null;
            await DB.Instance.sync();
        }

        private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            combobox1.ItemsSource = DB.Instance.users.Select(x => x.name);
            this.UpdateLayout();
        }

        private void AddCategory(object sender, RoutedEventArgs e)
        {
            AddCategory addCatWindow = new AddCategory();
            addCatWindow.ShowDialog();
        }

        private void AddSubCategory(object sender, RoutedEventArgs e)
        {
            AddSubCat subCatWindow = new AddSubCat();
            subCatWindow.ShowDialog();
        }

        private void AddRecord(object sender, RoutedEventArgs e)
        {
            if (DB.Instance.currentUser == null)
            {
                MessageBox.Show("Please select a user!");
                return;
            }
            AddRecord addRecordWindow = new AddRecord();
            addRecordWindow.ShowDialog();
            SetCurrentUser();
        }

        private void AddUser(object sender, RoutedEventArgs e)
        {
            AddUser addUser = new AddUser();
            addUser.Show();
            this.Close();
        }

        private void SetCurrentUser(object sender = null, RoutedEventArgs e = null)
        {
            DB.Instance.currentUser = DB.Instance.users[combobox1.SelectedIndex];
            yearPicker.SelectedIndex = -1;
            monthPicker.SelectedIndex = -1;
            records = getItems(DB.Instance.categories);
            years = records.Select(x => x.Date.Year).Distinct().ToList();
            if (years.Count == 0)
            {
                year = -1;
                datagrid.ItemsSource = null;
                currentLabel.Content = "current: 0.00";
                paidLable.Content = "Paid: 0.00";
                incomeLabel.Content = "Income: 0.00";
                years = new List<int>();
                months = new List<int>();
                yearPicker.ItemsSource = null;
                monthPicker.ItemsSource = null;
            }
            else
            {
                years.Sort();
                yearPicker.ItemsSource = years;
                if (years.Contains(DateTime.Now.Date.Year)) year = DateTime.Now.Date.Year;
                else year = years.Last();
                yearPicker.SelectedIndex = years.IndexOf(year);
            }

        }
        private List<Record> getItems(List<Category> cats)
        {
            List<Record> records = new List<Record>();
            foreach (Category cat in cats)
            {
                if (cat.items.Count > 0)
                {
                    records.AddRange(cat.items.Where(x => x.User.name == DB.Instance.currentUser.name));
                }
                if (cat.children.Count > 0)
                {
                    records.AddRange(getItems(cat.children));
                }
            }
            return (records.OrderBy(x => x.Date).ToList());
        }

        private void EditRecord(object sender, RoutedEventArgs e)
        {
            if (DB.Instance.currentUser == null)
            {
                MessageBox.Show("Please select a user!");
                return;
            }
            Record record = datagrid.SelectedItem as Record;
            if (record == null)
            {
                MessageBox.Show("Please select a record first.");
                return;
            }
            AddRecord editRecord = new AddRecord(record);
            editRecord.Show();
        }

        private void yearPicker_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (yearPicker.SelectedIndex == -1)
            {
                datagrid.ItemsSource = null;
                currentLabel.Content = "current: 0.00";
                paidLable.Content = "Paid: 0.00";
                incomeLabel.Content = "Income: 0.00";
            }
            else
            {
                year = years[yearPicker.SelectedIndex];
                records = getItems(DB.Instance.categories).Where(x => x.Date.Year == year).ToList();
                months = records.Select(x => x.Date.Month).Distinct().ToList();
                months.Add(0);
                months.Sort();
                month = 0;
                for (int i = DateTime.Now.Date.Month; i > 0; i--)
                {
                    if (months.Contains(i))
                    {
                        month = i;
                        break;
                    }
                }
                monthPicker.ItemsSource = null;
                monthPicker.ItemsSource = months;
                monthPicker.SelectedIndex = months.IndexOf(month);
                //MessageBox.Show(monthPicker.SelectedIndex.ToString());
            }
        }

        private void monthPicker_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (monthPicker.SelectedIndex == -1) monthPicker.SelectedIndex = months.Count - 1;
            else month = months[monthPicker.SelectedIndex];

            if (month == 0)
            {
                records = getItems(DB.Instance.categories).Where(x => x.Date.Year == year).ToList();
                datagrid.ItemsSource = null;
                datagrid.ItemsSource = records;
                income = records.Where(x => x.Transaction == cashFlow.Income).Select(x => x.Value).Sum();
                payments = records.Where(x => x.Transaction == cashFlow.Expense).Select(x => x.Value).Sum();
                current = income - payments;
                currentLabel.Content = "current: " + current.ToString();
                paidLable.Content = "Paid: " + payments.ToString();
                incomeLabel.Content = "Income: " + income.ToString();
                var c = datagrid.Columns[3] as DataGridTextColumn;
                //c.Binding = new Binding("Date");
                c.Binding.StringFormat = "yyyy-MM-dd";
            }
            else
            {
                records = getItems(DB.Instance.categories).Where(x => x.Date.Year == year).ToList();
                records = records.Where(x => x.Date.Month == month).ToList();
                datagrid.ItemsSource = null;
                datagrid.ItemsSource = records;
                income = records.Where(x => x.Transaction == cashFlow.Income).Select(x => x.Value).Sum();
                payments = records.Where(x => x.Transaction == cashFlow.Expense).Select(x => x.Value).Sum();
                current = income - payments;
                currentLabel.Content = "current: " + current.ToString();
                paidLable.Content = "Paid: " + payments.ToString();
                incomeLabel.Content = "Income: " + income.ToString();
                var c = datagrid.Columns[3] as DataGridTextColumn;
                //c.Binding = new Binding("Date");
                c.Binding.StringFormat = "yyyy-MM-dd";
            }
        }
    }
}
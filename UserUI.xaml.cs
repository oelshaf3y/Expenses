using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using static MaterialDesignThemes.Wpf.Theme;
using static System.Net.Mime.MediaTypeNames;

namespace Expenses
{
    /// <summary>
    /// Interaction logic for UserUI.xaml
    /// </summary>
    public partial class UserUI : Window
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
        public Person user;
        public UserUI(Person user)
        {
            InitializeComponent();
            datagrid.ItemsSource = new List<Record>();
            this.user = user;
            DB.Instance.setCurrentUser(user);
            SetCurrentUser(this.user);
            UserName.Content = DB.Instance.currentUser.name;
        }

        private void AddRecord(object sender, RoutedEventArgs e)
        {
            AddRecord addRecordWindow = new AddRecord(user);
            addRecordWindow.ShowDialog();
            int index = monthPicker.SelectedIndex;
            monthPicker.SelectedIndex = 0;
            monthPicker.SelectedIndex = index;
        }

        private void SetCurrentUser(Person user)
        {
            DB.Instance.setCurrentUser(user);
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

        private void EditRecord(object sender, RoutedEventArgs e)
        {
            //if (DB.Instance.currentUser == null)
            //{
            //    MessageBox.Show("Please select a user!");
            //    return;
            //}
            Record record = datagrid.SelectedItem as Record;
            if (record == null)
            {
                MessageBox.Show("Please select a record first.");
                return;
            }
            AddRecord editRecord = new AddRecord(record);
            editRecord.ShowDialog();
        }


        private List<Record> getItems(List<Category> cats)
        {
            List<Record> records = new List<Record>();
            foreach (Category cat in cats)
            {
                if (cat.items.Count > 0)
                {
                    records.AddRange(cat.items.Where(x => x.User.name == user.name));
                }
                if (cat.children.Count > 0)
                {
                    records.AddRange(getItems(cat.children));
                }
            }
            return (records.OrderBy(x => x.Date).ToList());
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
                categories.Clear();
                categories.Add("All");
                categories.AddRange(records.Select(x => x.Category).Distinct());
                catsComboBox.ItemsSource = categories;
                catsComboBox.SelectedIndex = -1;
                catsComboBox.SelectedIndex = 0;

            }
            else
            {
                records = getItems(DB.Instance.categories).Where(x => x.Date.Year == year).ToList();
                records = records.Where(x => x.Date.Month == month).ToList();
                categories.Clear();
                categories.Add("All");
                categories.AddRange(records.Select(x => x.Category).Distinct());
                catsComboBox.ItemsSource = categories;
                catsComboBox.SelectedIndex = -1;
                catsComboBox.SelectedIndex = 0;

            }
        }

        private void catsComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (catsComboBox.SelectedIndex == -1) return;
            string cat = catsComboBox.SelectedItem.ToString();
            List<Record> innerRecords = records;
            if (cat != "All")
            {
                innerRecords = records.Where(x => x.Category == cat).ToList();
            }
            datagrid.ItemsSource = null;
            datagrid.ItemsSource = innerRecords;
            income = records.Where(x => x.Transaction == cashFlow.Income).Select(x => x.Value).Sum();
            payments = records.Where(x => x.Transaction == cashFlow.Expense).Select(x => x.Value).Sum();
            current = income - payments;
            currentLabel.Content = "current: " + current.ToString();
            paidLable.Content = "Paid: " + payments.ToString();
            incomeLabel.Content = "Income: " + income.ToString();

            //c.Binding = new Binding("Date");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();
            this.Close();
            mw.ShowDialog();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Are you sure you want to delete the record?", "Delete! ", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                Record record = datagrid.SelectedItem as Record;
                Category category = DB.flatten(DB.Instance.categories).Where(x => x.getFullName() == record.Category).FirstOrDefault();
                category.RemoveItem(record);
                int index = monthPicker.SelectedIndex;
                monthPicker.SelectedIndex = 0;
                monthPicker.SelectedIndex = index;
                DB.Instance.sync();
            }

        }
    }
}

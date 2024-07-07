using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
using Button = System.Windows.Controls.Button;
using CheckBox = System.Windows.Controls.CheckBox;
using RadioButton = System.Windows.Controls.RadioButton;
using TabControl = System.Windows.Controls.TabControl;

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
        TabControl tabControl;
        List<Record> innerRecords = new List<Record>();
        List<string> categories = new List<string>();
        public Person user;
        public UserUI(Person user)
        {
            InitializeComponent();
            datagrid.ItemsSource = new List<Record>();
            this.user = user;
            DB.Instance.setCurrentUser(user);
            SetCurrentUser(this.user);
            //UserName.Content = this.user;
            //UserName.Icon = this.user.initials;
        }


        private void AddRecord(object sender, RoutedEventArgs e)
        {
            AddRecord addRecordWindow = new AddRecord(user);
            addRecordWindow.ShowDialog();
            Refresh();
        }

        private void Refresh()
        {
            TabItem t = tabControl.SelectedItem as TabItem;
            if (t.Content == null) { GetTabs(); return; }
            WrapPanel wp = t.Content as WrapPanel;
            for (int i = 0; i < wp.Children.Count; i++)
            {
                Picker mp = wp.Children[i] as Picker;
                if (mp.RB.IsChecked == true)
                {
                    if (i != 0)
                    {
                        Picker op = wp.Children[0] as Picker;
                        op.RB.IsChecked = true;
                        mp.RB.IsChecked = true;
                    }
                    else
                    {
                        Picker op = wp.Children[1] as Picker;
                        op.RB.IsChecked = true;
                        mp.RB.IsChecked = true;
                    }
                }
            }
        }

        private void SetCurrentUser(Person user)
        {
            DB.Instance.setCurrentUser(user);
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
            }
            else
            {
                years.Sort((x, y) => y.CompareTo(x));
                //yearPicker.ItemsSource = years;

                //if (years.Contains(DateTime.Now.Date.Year)) year = DateTime.Now.Date.Year;
                //else year = years.First();
                //yearPicker.SelectedIndex = years.IndexOf(year);
            }
            GetTabs();

        }

        private void GetTabs()
        {
            if (msp.Children.Count > 0)
            {
                msp.Children.RemoveAt(0);
                UpdateLayout();
            }
            Header tc = new Header();
            tabControl=tc.tabControl;
            Button back = new Button();
            back.Click += this.back;
            PackIcon butIco = new PackIcon();
            butIco.Kind = PackIconKind.ArrowBack;
            back.Content = butIco;
            back.Width = 50;
            StackPanel sp = new StackPanel();
            sp.Children.Add(back);
            TabItem t = new TabItem();
            t.Header = sp;
            tabControl.Items.Add(t);
            foreach (int y in years)
            {
                records = getItems(DB.Instance.categories).Where(x => x.Date.Year == y).ToList();
                months = records.Select(x => x.Date.Month).Distinct().ToList();
                months.Add(0);
                months.Sort((x, y) => y.CompareTo(x));
                TabItem tab = new TabItem();
                tab.Header = y.ToString();
                tabControl.SelectionChanged += (object sender, SelectionChangedEventArgs e) =>
                {
                    ChangeYear(sender, e);
                };
                WrapPanel wp = new WrapPanel();
                string GroupName = y.ToString();
                foreach (int m in months)
                {
                    Picker mp = new Picker(y, m);

                    mp.RB.Checked += ChangeMonth;
                    wp.Children.Add(mp);
                }
                tab.Content = wp;
                tabControl.Items.Add(tab);
            }
            UserHolder userholder = new UserHolder(null);
            userholder.func = () =>
            {
                this.Close();
                return true;
            };
            userholder.userchip.IsDeletable = true;
            userholder.userchip.Content = user.name;
            userholder.userchip.Icon = user.initials;
            TabItem t2 = new TabItem();
            t2.Header = userholder;
            t2.Height = 60;
            tabControl.Items.Add(t2);
            TabItem activeTab = tabControl.Items[1] as TabItem;
            activeTab.IsSelected = true;
            msp.Children.Add(tc);
        }



        private void EditRecord(object sender, RoutedEventArgs e)
        {
            Record record = datagrid.SelectedItem as Record;
            if (record == null)
            {
                MessageBox.Show("Please select a record first.");
                return;
            }
            AddRecord editRecord = new AddRecord(record);
            editRecord.ShowDialog();
            Refresh();
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

        private void ChangeYear(object sender, SelectionChangedEventArgs e)
        {
            TabControl tc = (TabControl)sender;
            TabItem t = tc.SelectedItem as TabItem;
            if (t == tc.Items[0])
            {
                back(null, null);
            }
            else if (t == tc.Items[tc.Items.Count - 1])
            {
                return;
            }
            if (!Int32.TryParse(t.Header.ToString(), out year))
            {
                datagrid.ItemsSource = null;
                currentLabel.Content = "current: 0.00";
                paidLable.Content = "Paid: 0.00";
                incomeLabel.Content = "Income: 0.00";
                return;
            }
            else
            {
                records = getItems(DB.Instance.categories).Where(x => x.Date.Year == year).ToList();
                months = records.Select(x => x.Date.Month).Distinct().ToList();
            }
            if (t.Content is WrapPanel)
            {
                WrapPanel wrap = (WrapPanel)t.Content;
                List<Picker> pickers = new List<Picker>();
                foreach (Picker item in wrap.Children)
                {
                    pickers.Add(item);
                }
                pickers.OrderByDescending(x => x.RB.Content).Last().RB.IsChecked = true;
                pickers.OrderByDescending(x => x.RB.Content).First().RB.IsChecked = true;
            }
        }
        private void ChangeMonth(object sender, RoutedEventArgs e)
        {
            month = 0;
            RadioButton radioButton = sender as RadioButton;
            if (!Int32.TryParse(radioButton.Content.ToString().Split(":")[0], out month))
            {
                return;
            }
            categories.Clear();
            if (month == 0)
            {
                records = getItems(DB.Instance.categories).Where(x => x.Date.Year == year).ToList();
                categories.AddRange(records.Select(x => x.Category).Distinct());

            }
            else
            {
                records = getItems(DB.Instance.categories)
                    .Where(x => x.Date.Year == year && x.Date.Month == month).ToList();
                categories.AddRange(records.Select(x => x.Category).Distinct());
            }
            categories.Add("All");
            innerRecords = records;
            footer.Children.Clear();
            foreach (string category in categories)
            {
                Checker checker = new Checker();
                checker.CB.Content = category;
                checker.CB.Checked += CatFilter;
                checker.CB.Unchecked += CatFilter;
                footer.Children.Add(checker);

            }
            datagrid.ItemsSource = null;
            datagrid.ItemsSource = records;
            income = records.Where(x => x.Transaction == cashFlow.Income).Select(x => x.Value).Sum();
            payments = records.Where(x => x.Transaction == cashFlow.Expense).Select(x => x.Value).Sum();
            current = income - payments;
            currentLabel.Content = "current: " + current.ToString();
            paidLable.Content = "Paid: " + payments.ToString();
            incomeLabel.Content = "Income: " + income.ToString();
        }

        private void CatFilter(object sender, RoutedEventArgs e)
        {
            List<string> openCats = new List<string>();
            CheckBox CB = sender as CheckBox;
            if (CB.Content == "All")
            {
                foreach (Checker checker in footer.Children)
                {
                    if (checker.CB.Content == "All") continue;
                    checker.CB.IsChecked = CB.IsChecked;
                }
            }
            else
            {
                foreach (Checker checker in footer.Children)
                {
                    CheckBox CB2 = checker.CB as CheckBox;
                    if (CB2.IsChecked == false) innerRecords = innerRecords.Except(records.Where(x => x.Category == CB2.Content.ToString())).ToList();
                    else innerRecords.AddRange(
                        records.Where(x => x.Category == CB2.Content.ToString())
                        );
                }
            }
            datagrid.ItemsSource = null;
            datagrid.ItemsSource = innerRecords.OrderBy(x => x.Date).Distinct();
        }

        private void back(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();
            this.Close();
            mw.ShowDialog();
        }

        private void DeleteRecord(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Are you sure you want to delete the record?", "Delete! ", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                Record record = datagrid.SelectedItem as Record;
                Category category = DB.flatten(DB.Instance.categories).Where(x => x.getFullName() == record.Category).FirstOrDefault();
                category.RemoveItem(record);
                DB.Instance.sync();
            }
            Refresh();
        }
    }
}

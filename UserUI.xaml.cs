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
using LiveCharts;
using LiveCharts.Wpf;
using static MaterialDesignThemes.Wpf.Theme;
using static System.Net.Mime.MediaTypeNames;
using Button = System.Windows.Controls.Button;
using CheckBox = System.Windows.Controls.CheckBox;
using RadioButton = System.Windows.Controls.RadioButton;
using TabControl = System.Windows.Controls.TabControl;
using System.Windows.Ink;
using System.IO;
using System.Text.Json;

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
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            DB.Instance.setCurrentUser(user);
            SetCurrentUser(this.user);
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
                years = new List<int>();
                months = new List<int>();
            }
            else
            {
                years.Sort((x, y) => y.CompareTo(x));
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
            tabControl = tc.tabControl;
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
                tabControl.SelectionChanged += ChangeYear;

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
        private void DrawCharts()
        {

            Pie.Series.Clear();
            SumPie.Series.Clear();
            Line.Series.Clear();
            PieSeries expSer= new PieSeries();
            expSer.Title = "Expenses";
            double expenses = records.Where(x => x.Transaction == cashFlow.Expense).Select(x => x.Value).Sum();
            expSer.Values = new ChartValues<double> { expenses };
            expSer.DataLabels = true;
            SumPie.Series.Add(expSer);
            PieSeries incSer = new PieSeries();
            incSer.Title = "Income";
            incSer.DataLabels = true;
            double netIncome = records.Where(x => x.Transaction == cashFlow.Income).Select(x => x.Value).Sum();
            incSer.Values = new ChartValues<double> { netIncome };
            SumPie.Series.Add(incSer);
            PieSeries netSer = new PieSeries();
            netSer.Title = "Remainder";
            netSer.Values = new ChartValues<double> { netIncome-expenses};
            netSer.DataLabels = true;
            SumPie.Series.Add(netSer);
            SumPie.LegendLocation= LegendLocation.Bottom;
            //foreach (string category in categories.Distinct())
            foreach (string category in DB.Instance.categories.Select(x => x.name))
            {
                double value = records.Where(x => x.Category.Contains(category) && x.Transaction != cashFlow.Income).Select(x => x.Value).Sum();
                if (value == 0) continue;
                PieSeries series = new PieSeries();
                series.Title = category;
                series.Values = new ChartValues<Double> { value };
                Pie.Series.Add(series);
            }
            var a = new Axis();
            a.Labels = records.Select(x => x.Date.ToString("d")).Distinct().ToList();
            var b = new Axis();
            //b.LabelFormatter=new Func<double, string>= ()=>{

            //}
            var expensesVals = new ChartValues<double>();
            var incomeVals = new ChartValues<double>();
            var NetVals = new ChartValues<double>();
            foreach (string date in a.Labels)
            {
                double v = records.Where(x => x.Date.ToString("d") == date && x.Transaction == cashFlow.Expense)
                    .Select(x => x.Value).ToList().Sum();
                if (expensesVals.Count() > 0) expensesVals.Add(v + expensesVals.Last());
                else expensesVals.Add(v);
                double v2 = records.Where(x => x.Date.ToString("d") == date && x.Transaction == cashFlow.Income)
                    .Select(x => x.Value).Sum();
                if (incomeVals.Count() > 0) incomeVals.Add(v2 + incomeVals.Last());
                else incomeVals.Add(v2);
                NetVals.Add(incomeVals.Last() - expensesVals.Last());
            }
            a.Labels = a.Labels.Select(x => string.Join("/", x.Split('/')[0], x.Split('/')[1])).ToList();
            Line.AxisX = new AxesCollection() { a };
            Line.AxisY = new AxesCollection() { b };
            var expansesLine = new LineSeries();
            expansesLine.Values = expensesVals;
            expansesLine.Title = "Expenses";
            Line.Series.Add(expansesLine);
            var incomeLine = new LineSeries();
            incomeLine.Values = incomeVals;
            incomeLine.Title = "Income";
            Line.Series.Add(incomeLine);
            var netLine = new LineSeries();
            netLine.Values = NetVals;
            netLine.Title = "Net";
            Line.Series.Add(netLine);
        }
        private void AddRecord(object sender, RoutedEventArgs e)
        {
            AddRecord addRecordWindow = new AddRecord(user);
            addRecordWindow.ShowDialog();
            Refresh();
        }
        private void ExportRecords(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("You are about to export the data to excel file\nAre you sure?!", "Export! ",
                MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.No) return;
            Export exp = new Export(innerRecords);
        }
        private void EditRecord(object sender, RoutedEventArgs e)
        {
            Record record = datagrid.SelectedItem as Record;
            if (record == null)
            {
                MessageBox.Show("Please select a record first.","Selection?",MessageBoxButton.OK,MessageBoxImage.Exclamation);
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
                pickers.OrderByDescending(x => x.RB.Content).First().RB.IsChecked = false;
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
            DrawCharts();
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
                    if(checker.CB.IsChecked==true) openCats.Add(checker.CB.Content.ToString());
                }
            }
            else
            {
                foreach (Checker checker in footer.Children)
                {
                    CheckBox CB2 = checker.CB as CheckBox;
                    if (CB2.IsChecked == true) openCats.Add(CB2.Content.ToString());
                }
            }
            innerRecords=records.Where(x=>openCats.Contains(x.Category)).ToList();
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
            if (datagrid.SelectedItem == null)
            {
                MessageBox.Show("You Haven't selected any!", "selection", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }
            var result = MessageBox.Show("Are you sure you want to delete the record?", "Delete! ",
                MessageBoxButton.YesNo, MessageBoxImage.Error);
            if (result == MessageBoxResult.Yes)
            {
                Record record = datagrid.SelectedItem as Record;
                Category category = DB.flatten(DB.Instance.categories).Where(x => x.getFullName() == record.Category).FirstOrDefault();
                category.RemoveItem(record);
                DB.Instance.sync();
            Refresh();
            }
        }
    }
}

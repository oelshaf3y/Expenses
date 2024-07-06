using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Expenses
{
    /// <summary>
    /// Interaction logic for AddRecord.xaml
    /// </summary>
    public partial class AddRecord : Window
    {

        public ShoppingList list { get; set; }
        private bool isEditable = false;
        public Record record;
        public Person user;
        public AddRecord(Person user)
        {
            InitializeComponent();
            GetCatTree(null, DB.Instance.categories);
            this.user = user;
        }

        public AddRecord(Record record)
        {
            InitializeComponent();
            this.record = record;
            this.user = record.User;
            isEditable = true;
            GetCatTree(null, DB.Instance.categories);
            TreeViewItem item = FindItem(record.Category.Split('.').Last(), CatTree);
            if (item != null) item.IsSelected = true;
            //combobox1.ItemsSource = cats;
            //combobox1.SelectedIndex = cats.IndexOf(record.Category);
            textBox1.Text = record.Info;
            textbox2.Text = record.Value.ToString();
            if (record.Transaction == cashFlow.Income) income.IsChecked = true;
            DPicker.SelectedDate = record.Date;
            if (record.Transaction == cashFlow.Income) income.IsChecked = true;
            if (record.GroceryList != null)
            {
                this.list = record.GroceryList;
                dataGrid.ItemsSource = list.items;
                textbox2.Text = list.Value.ToString();
                textbox2.IsEnabled = false;
                addListButIco.Kind = PackIconKind.Edit;
            }
        }


        private void SaveRecord(object sender, RoutedEventArgs e)
        {
            if (CatTree.SelectedItem == null)
            {
                MessageBox.Show("Please Enter the Info!");
                return;
            }
            DateTime date;
            cashFlow transaction = cashFlow.Expense;
            if (DPicker.SelectedDate == null)
            {
                date = DateTime.Now;
            }
            else
            {
                date = (DateTime)DPicker.SelectedDate;
            }
            if (double.TryParse(textbox2.Text, out double result))
            {
                TreeViewItem tvi = CatTree.SelectedItem as TreeViewItem;
                Category category = DB.flatten(DB.Instance.categories).Where(x => x.name == tvi.Header.ToString()).FirstOrDefault();
                if (isEditable)
                {
                    record.Info = textBox1.Text;
                    record.Value = result;
                    record.Category = category.getFullName();
                    record.Date = date;
                    record.Transaction = transaction;
                    record.GroceryList = list;
                    if (income.IsChecked == true) record.Transaction = cashFlow.Income;

                }
                else
                {

                    if (income.IsChecked == true) transaction = cashFlow.Income;
                    category.AddItem(
                        new Record(textBox1.Text, result, category.getFullName(), date.Date, transaction, user,
                        list)
                        );
                }
                DB.Instance.sync();
                this.Close();
            }
            else
            {
                MessageBox.Show("Enter a number in the amount box!");
                return;
            }
        }

        private void checkbox1_Checked(object sender, RoutedEventArgs e)
        {
            if (DPicker != null)
                DPicker.SelectedDate = DateTime.Now.Date;
        }

        private void checkbox1_Unchecked(object sender, RoutedEventArgs e)
        {
            DPicker.SelectedDate = null;
        }

        private void AddList(object sender, RoutedEventArgs e)
        {
            if (textBox1.Text.Length == 0 || textBox1.Text == null)
            {
                MessageBox.Show("Enter info first!");
                return;
            }
            if (list != null)
            {
                AddGroceryList addList = new AddGroceryList(list);
                addList.populateList(list.items);
                addList.ShowDialog();
            }
            else
            {
                list = new ShoppingList(textBox1.Text);
                AddGroceryList addList = new AddGroceryList(list);
                addList.ShowDialog();
            }
            if (list.items.Count > 0)
            {
                textbox2.Text = list.Value.ToString();
                textbox2.IsEnabled = false;
                dataGrid.ItemsSource = list.items;
                addListButIco.Kind = PackIconKind.Edit;
            }
            else
            {
                textbox2.Text = "";
                textbox2.IsEnabled = true;
                dataGrid.ItemsSource = list.items;
                list = null;
                addListButIco.Kind= PackIconKind.Cart;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            AddCategory addCatWindow = new AddCategory(this);
            addCatWindow.ShowDialog();
            //this.Close();
        }
        private TreeViewItem FindItem(string category, object Parent)
        {
            if (Parent is TreeView)
            {
                TreeView tv = (TreeView)Parent;
                foreach (TreeViewItem item in tv.Items)
                {
                    if (item.Header.ToString() == category)
                    {
                        return item;
                    }
                    else if (item.Items.Count > 0)
                    {
                        var a = FindItem(category, item);
                        if (a != null)
                        {
                            item.IsExpanded = true;
                            return a;
                        }
                    }
                }
            }
            else
            {
                TreeViewItem parent = Parent as TreeViewItem;
                foreach (TreeViewItem item in parent.Items)
                {
                    if (item.Header.ToString() == category)
                    {
                        return item;
                    }
                    else if (item.Items.Count > 0)
                    {
                        var a = FindItem(category, item);
                        if (a != null) { item.IsExpanded = true; return a; }
                    }
                }
            }
            return null;
        }
        private void GetCatTree(TreeViewItem parent, List<Category> categories)
        {
            if (parent == null)
            {
                foreach (Category cat in categories)
                {
                    TreeViewItem item = new TreeViewItem();
                    item.Header = cat.name;
                    CatTree.Items.Add(item);
                    if (cat.children.Count > 0)
                    {
                        GetCatTree(item, cat.children);
                    }
                }
            }
            else
            {
                foreach (Category cat in categories)
                {
                    TreeViewItem item = new TreeViewItem();
                    item.Header = cat.name;
                    parent.Items.Add(item);
                    if (cat.children.Count > 0)
                    {
                        GetCatTree(item, cat.children);
                    }
                }
            }
        }
    }
}

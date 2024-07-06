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
            checkbox1.IsChecked = true;
            combobox1.ItemsSource = DB.flatten(DB.Instance.categories).Select(x => x.getFullName());
            this.user = user;
        }

        public AddRecord(Record record)
        {
            this.record = record;
            this.user = record.User;
            isEditable = true;
            InitializeComponent();
            List<string> cats = DB.flatten(DB.Instance.categories).Select(x => x.getFullName()).ToList();
            combobox1.ItemsSource = cats;
            combobox1.SelectedIndex = cats.IndexOf(record.Category);
            textBox1.Text = record.Info;
            textbox2.Text = record.Value.ToString();
            checkbox1.IsChecked = false;
            if(record.Transaction==cashFlow.Income) income.IsChecked = true;
            checkbox1.Visibility = Visibility.Hidden;
            datePicker.SelectedDate = record.Date;
            if (record.Transaction == cashFlow.Income) income.IsChecked = true;
            if (record.GroceryList != null)
            {
                this.list = record.GroceryList;
                dataGrid.ItemsSource = list.items;
                textbox2.Text = list.Value.ToString();
                textbox2.IsEnabled = false;
                addListBut.Content = "Edit List";
            }
        }

        private void SaveRecord(object sender, RoutedEventArgs e)
        {
            if (combobox1.SelectedIndex == -1) { MessageBox.Show("Please select a category!"); return; }
            if (textBox1.Text.Length == 0 || textBox1.Text == null)
            {
                MessageBox.Show("Please Enter the Info!");
                return;
            }
            DateTime date;
            cashFlow transaction = cashFlow.Expense;
            if (checkbox1.IsChecked == true)
            {
                date = DateTime.Now;
            }
            else
            {
                date = (DateTime)datePicker.SelectedDate;
            }
            if (double.TryParse(textbox2.Text, out double result))
            {
                Category category = DB.flatten(DB.Instance.categories)[combobox1.SelectedIndex];
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
                //user.adjustBalance(index,result);
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
            datePicker.Visibility = Visibility.Hidden;
        }

        private void checkbox1_Unchecked(object sender, RoutedEventArgs e)
        {
            datePicker.Visibility = Visibility.Visible;

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
                addListBut.Content = "Edit List";
            }
            else
            {
                textbox2.Text = "";
                textbox2.IsEnabled = true ;
                dataGrid.ItemsSource = list.items;
                list = null;
                addListBut.Content = "Add List";
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
    }
}

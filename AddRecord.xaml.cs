using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace Expenses
{
    /// <summary>
    /// Interaction logic for AddRecord.xaml
    /// </summary>
    public partial class AddRecord : Window
    {
        public ShoppingList list { get; set; }
        public AddRecord()
        {
            InitializeComponent();
            combobox1.ItemsSource = DB.flatten(DB.Instance.categories).Select(x => x.getFullName());
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
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
                if (income.IsChecked == true) transaction = cashFlow.Income;
                category.AddItem(
                    new Record(textBox1.Text, result, category.getFullName(), date.Date, transaction, DB.Instance.currentUser, list));
                //DB.Instance.currentUser.adjustBalance(index,result);
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
        }
    }
}

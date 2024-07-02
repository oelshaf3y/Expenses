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

namespace Expenses
{
    /// <summary>
    /// Interaction logic for AddRecord.xaml
    /// </summary>
    public partial class AddRecord : Window
    {
        public AddRecord()
        {
            InitializeComponent();
            combobox1.ItemsSource = DB.flatten(DB.Instance.categories).Select(x => x.getFullName());
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DateTime date;
            int index = -1;
            if (checkbox1.IsChecked==true)
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
                if (income.IsChecked == true) index = 1; 
                category.AddItem(new Record(textBox1.Text, result, category.getFullName(),date.Date,index));
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
            datePicker.Visibility =Visibility.Hidden;
        }

        private void checkbox1_Unchecked(object sender, RoutedEventArgs e)
        {
            datePicker.Visibility = Visibility.Visible;

        }
    }
}

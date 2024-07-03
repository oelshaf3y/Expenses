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
    /// Interaction logic for AddSubCat.xaml
    /// </summary>
    public partial class AddSubCat : Window
    {
        private Category category { get; set; }
        public AddSubCat()
        {
            InitializeComponent();
            combobox1.ItemsSource = DB.flatten(DB.Instance.categories).Select(x => x.name);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            category = DB.flatten(DB.Instance.categories)[combobox1.SelectedIndex];
            category.AddSubCategory(new Category( textBox1.Text, textBox2.Text, parent: category.name));
            DB.Instance.sync();
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}

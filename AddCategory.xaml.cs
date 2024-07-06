using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
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
    /// Interaction logic for AddCategory.xaml
    /// </summary>
    public partial class AddCategory : Window
    {
        private Category category { get; set; }
        AddRecord parent { get; set; }
        public List<String> categories = new List<string> { "None" };
        public AddCategory(AddRecord Parent)
        {
            this.parent= Parent;
            InitializeComponent();
            categories.AddRange(DB.flatten(DB.Instance.categories).Select(x => x.name));
            combobox1.ItemsSource = categories;
            combobox1.SelectedIndex = 0;
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            if (textBox1.Text.Length == 0)
            {

                MessageBox.Show("Please enter category name!");
                return;
            }
            if (combobox1.SelectedIndex == 0)
            {
                Category cat = new Category(textBox1.Text, textBox2.Text);
                DB.Instance.categories.Add(cat);
                DB.Instance.sync();
            }
            else
            {
                category = DB.flatten(DB.Instance.categories)[combobox1.SelectedIndex - 1];
                category.AddSubCategory(new Category(textBox1.Text, textBox2.Text, parent: category.name));
                DB.Instance.sync();
            }
            this.parent.Close();
            this.Close();
            AddRecord addrecord = new AddRecord(parent.user);
            addrecord.ShowDialog();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}

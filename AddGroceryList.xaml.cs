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
    /// Interaction logic for AddGroceryList.xaml
    /// </summary>
    public partial class AddGroceryList : Window
    {
        public ObservableCollection<ShopingItem> items { get; set; } = new ObservableCollection<ShopingItem>();
        public ShoppingList parent;
        public AddGroceryList(ShoppingList parent)
        {
            this.parent = parent;
            InitializeComponent();
            dataGrid.ItemsSource = items;
            dataGrid.CanUserAddRows = true;
            //Button_Click_1(null,null);
        }

        public void populateList(List<ShopingItem> shopingList)
        {
            foreach (ShopingItem item in shopingList)
            {
                items.Add(item);
            }
        }
        private void Cancel(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


        private void AddRow(object sender, RoutedEventArgs e)
        {
            items.Add(new ShopingItem("   "));
        }

        private void Save(object sender, RoutedEventArgs e)
        {
            parent.items = items.ToList();
            parent.Value = items.Select(x => x.Value * x.Count).Sum();
            this.Close();
        }
        private void AddRow_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.OemPlus || e.Key == Key.Add)
            {
                AddRow(null, null);
            }
        }
        private void RemoveRow(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItem == null) return;
            var row = dataGrid.SelectedItem as ShopingItem;
            items.Remove(row);
        }
    }
}

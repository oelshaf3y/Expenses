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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Expenses
{
    /// <summary>
    /// Interaction logic for MonthPicker.xaml
    /// </summary>
    public partial class Picker : UserControl
    {
        public int year { get; set; }
        public int month { get; set; }
        public Picker(int year,int month)
        {
            this.year = year;
            this.month = month;
            InitializeComponent();
            RB.Content = GetMonth(month);
            RB.GroupName = year.ToString();
        }
        private string GetMonth(int m)
        {
            List<string> monthNames = new List<string>() {"All",
                "Jan","Feb","Mar",
                "Apr","May","Jun",
                "Jul","Aug","Sep",
                "Oct","Nov","Dec"
            };

            return m.ToString() + ": " + monthNames[m];
        }
    }
}

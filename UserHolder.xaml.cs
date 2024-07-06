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
using static MaterialDesignThemes.Wpf.Theme;

namespace Expenses
{
    /// <summary>
    /// Interaction logic for UserHolder.xaml
    /// </summary>
    public partial class UserHolder : UserControl
    {
        MainWindow mw;
        public UserHolder(MainWindow mw)
        {
            this.mw = mw;
            InitializeComponent();
        }
        private void SetCurrentUser(object sender = null, RoutedEventArgs e = null)
        {
            try
            {

                Person user = DB.Instance.users.Where(x => this.userchip.Content == x.name).First();
                DB.Instance.setCurrentUser(user);
                UserUI ui = new UserUI(user);
                mw.Close();
                ui.ShowDialog();
            }
            catch
            {
                return;
            }
        }
    }
}

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

namespace UserInterface.Pages
{
    /// <summary>
    /// Logique d'interaction pour BTC.xaml
    /// </summary>
    public partial class BTC : Page
    {
        public BTC()
        {
            InitializeComponent();
        }

        private void backHomePage(object sender, RoutedEventArgs e)
        {
            Page homePage = new Landingpage();
            this.NavigationService.Navigate(homePage);

        }
    }
}

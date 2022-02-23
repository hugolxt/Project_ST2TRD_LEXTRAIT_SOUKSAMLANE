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
using MaterialDesignThemes.Wpf;
using System.Diagnostics;
using UserInterface.Pages;
using LiveCharts;
using LiveCharts.Wpf;

namespace UserInterface.Pages
{
    /// <summary>
    /// Logique d'interaction pour Landingpage.xaml
    /// </summary>
    public partial class Landingpage : Page
    {
        public Landingpage()
        {
            InitializeComponent();
            setUpVolume();
        }
        public bool IsDarktheme { get; set; }
        private readonly PaletteHelper paletteHelper = new PaletteHelper();
        private void toggleTheme(object sender, RoutedEventArgs e)
        {
            ITheme theme = paletteHelper.GetTheme();
            if (IsDarktheme == true)
            {
                Trace.WriteLine("LIGHT");
                IsDarktheme = false;
                theme.SetBaseTheme(Theme.Light);
            }
            else
            {
                Trace.WriteLine("DARK");
                IsDarktheme = true;
                theme.SetBaseTheme(Theme.Dark);
            }
            paletteHelper.SetTheme(theme);
        }

        private void exitApp(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        
        private void navigate(object sender, RoutedEventArgs e)
        {
            Trace.WriteLine("Navigate");
            //NavigationService nav = NavigationService.GetNavigationService(this);
            //nav.Navigate(new Uri("Page1.xaml", UriKind.RelativeOrAbsolute));

        }

        private void goToBTC(object sender, RoutedEventArgs e)
        {
            Page btc = new BTC();
            this.NavigationService.Navigate(btc);
        }
        private void goToETH(object sender, RoutedEventArgs e)
        {
            Page eth = new ETH();
            this.NavigationService.Navigate(eth);
        }

        private void setUpVolume()
        {
            btcVolume.Text = "10";
            ethVolume.Text = "30";
        }
    }
}

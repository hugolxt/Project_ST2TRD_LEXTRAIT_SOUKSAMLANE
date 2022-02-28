using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using MaterialDesignThemes.Wpf;
using System.Diagnostics;

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
            setData();
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
        private void goToXRP(object sender, RoutedEventArgs e)
        {
            Page xrp = new XRP();
            this.NavigationService.Navigate(xrp);
        }

        private void goToSOL(object sender, RoutedEventArgs e)
        {
            Page sol = new SOL();
            this.NavigationService.Navigate(sol);
        }

        private void goToBNB(object sender, RoutedEventArgs e)
        {
            Page bnb = new BNB();
            this.NavigationService.Navigate(bnb);
        }

        private void goToCRO(object sender, RoutedEventArgs e)
        {
            Page cro = new CRO();
            this.NavigationService.Navigate(cro);
        }

        private void goToADA(object sender, RoutedEventArgs e)
        {
            Page ada = new ADA();
            this.NavigationService.Navigate(ada);
        }

        private void goToAVAX(object sender, RoutedEventArgs e)
        {
            Page avax = new AVAX();
            this.NavigationService.Navigate(avax);
        }

        private void goToDOT(object sender, RoutedEventArgs e)
        {
            Page dot = new DOT();
            this.NavigationService.Navigate(dot);
        }
        private void gotoMATIC(object sender, RoutedEventArgs e)
        {
            Page matic = new MATIC();
            this.NavigationService.Navigate(matic);
        }


        //  Bullish = LimeGreen
        //  neutral = Gray
        //  bearish = Brown
        private void updateTrendElement(Card cardTrend, TextBlock textBlockTrend, string trend)
        {
            SolidColorBrush color;
            switch (trend)
            {
                case "Bullish":
                    color = Brushes.LimeGreen;
                    break;
                case "Neutral":
                    color = Brushes.Gray;
                    break ;
                case "Bearish":
                    color = Brushes.Brown;
                    break;
                default:
                    color = Brushes.Gray;
                    break;
            }
            cardTrend.Background = color;
            textBlockTrend.Text = trend;
        }
        
        private void updateRowTrends(Card card1, TextBlock text1, Card card2, TextBlock text2, Card card3, TextBlock text3, Crypto crypto)
        {
            updateTrendElement(card1, text1, crypto_data.growthCalculator(crypto, 1));
            updateTrendElement(card2, text2, crypto_data.growthCalculator(crypto, 14));
            updateTrendElement(card3, text3, crypto_data.growthCalculator(crypto, 30));
        }
        

        
        private void setData()
        {
            int period = 365; // period we need in days

            //  get all our datas about targated assets
            Crypto BTC = crypto_data.GetDailyPrice("BTC", period);
            Crypto ETH = crypto_data.GetDailyPrice("ETH", period);
            Crypto XRP = crypto_data.GetDailyPrice("XRP", period);
            Crypto SOL = crypto_data.GetDailyPrice("SOL", period);
            Crypto BNB = crypto_data.GetDailyPrice("BNB", period);
            Crypto CRO = crypto_data.GetDailyPrice("CRO", period);
            Crypto ADA = crypto_data.GetDailyPrice("ADA", period);
            Crypto AVAX = crypto_data.GetDailyPrice("AVAX", period);
            Crypto DOT = crypto_data.GetDailyPrice("DOT", period);
            Crypto MATIC = crypto_data.GetDailyPrice("MATIC", period);

            // Calculate and set the Volume on the full period given
            btcVolume.Text = crypto_data.TotalVolumeCalculator(BTC);
            ethVolume.Text = crypto_data.TotalVolumeCalculator(ETH);
            xrpVolume.Text = crypto_data.TotalVolumeCalculator(XRP);
            solVolume.Text = crypto_data.TotalVolumeCalculator(SOL);
            bnbVolume.Text = crypto_data.TotalVolumeCalculator(BNB);
            croVolume.Text = crypto_data.TotalVolumeCalculator(CRO);
            adaVolume.Text = crypto_data.TotalVolumeCalculator(ADA);
            avaxVolume.Text = crypto_data.TotalVolumeCalculator(AVAX);
            dotVolume.Text = crypto_data.TotalVolumeCalculator(DOT);
            maticVolume.Text = crypto_data.TotalVolumeCalculator(MATIC);

            // Determine and set an attribute "Bearish" or Bullish" for each period and each asset
            updateRowTrends(btcTrend1, btcTrend1Name, btcTrend2, btcTrend2Name, btcTrend3, btcTrend3Name, BTC);
            updateRowTrends(ethTrend1, ethTrend1Name, ethTrend2, ethTrend2Name, ethTrend3, ethTrend3Name, ETH);
            updateRowTrends(xrpTrend1, xrpTrend1Name, xrpTrend2, xrpTrend2Name, xrpTrend3, xrpTrend3Name, XRP);
            updateRowTrends(solTrend1, solTrend1Name, solTrend2, solTrend2Name, solTrend3, solTrend3Name, SOL);
            updateRowTrends(bnbTrend1, bnbTrend1Name, bnbTrend2, bnbTrend2Name, bnbTrend3, bnbTrend3Name, BNB);
            updateRowTrends(croTrend1, croTrend1Name, croTrend2, croTrend2Name, croTrend3, croTrend3Name, CRO);
            updateRowTrends(adaTrend1, adaTrend1Name, adaTrend2, adaTrend2Name, adaTrend3, adaTrend3Name, ADA);
            updateRowTrends(avaxTrend1, avaxTrend1Name, avaxTrend2, avaxTrend2Name, avaxTrend3, avaxTrend3Name, AVAX);
            updateRowTrends(dotTrend1, dotTrend1Name, dotTrend2, dotTrend2Name, dotTrend3, dotTrend3Name, DOT);
            updateRowTrends(maticTrend1, maticTrend1Name, maticTrend2, maticTrend2Name, maticTrend3, maticTrend3Name, MATIC);


        }

        
    }
}

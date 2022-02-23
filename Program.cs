using System;
using System.Collections.Generic;
using System.Linq;

namespace ST2TRD_Project_SOUKSAMLANE_LEXTRAIT
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("////////// The application has started... \\\\\\\\\\\n");

            string[] tickers = { "BTC", "ETH", "XRP", "SOL", "BNB", "CRO", "ADA", "AVAX", "DOT", "MATIC" };
            getYearlyTotalVol(tickers);

            DailyPrice BTC = GetDailyPrice("BTC", 30);
            DailyPrice ETH = GetDailyPrice("ETH", 30);
            DailyPrice XRP = GetDailyPrice("XRP", 30);
            DailyPrice SOL = GetDailyPrice("SOL", 30);

            Datum.altcoinsCorrelation(BTC, ETH, "12/02/2022", "20/02/2022", "Close");
            Datum.altcoinsCorrelation(XRP, SOL, "12/02/2022", "20/02/2022", "Close");

            Datum.calculateDailyMarketCap(BTC);
            Datum.calculateDailyMarketCap(SOL);                     

            Console.WriteLine("\n////////// The application has ended... \\\\\\\\\\");
        }

        public static IDictionary<string, double> getYearlyTotalVol(string[] tickers)
        {
            //This function takes a list of few tickers' names, calls an other function which calculates and returns the yearly total
            //volum of each cryptocurrency in a dictionary format and finally calls an other function to display the dictionary
            Console.WriteLine("~Here below, ten of the biggest cryptocurrency's total volume traded since one year (in US dollar) : \n");
            IDictionary<string, double> yearlyAverageVol = new Dictionary<string, double>();
          
            List<string> tickersList = new List<string>(tickers);

            for (int counter = 0; counter < tickersList.Count; counter++)
            {
                string tick = tickersList[counter];
                DailyPrice Crypto = GetDailyPrice(tickersList[counter], 365);
                Datum.yearlyVolumeCalculator(Crypto, yearlyAverageVol, tickersList[counter]);
            }
            Datum.displayDictionnary(yearlyAverageVol,"Cryptocurrency","Total yearly volume");
            return yearlyAverageVol;
        }

        public static DailyPrice GetDailyPrice(string ticker, int period)
        {
            String json = new GetDataUrl().Getdata(new GetDataUrl().UrlGenerator(ticker, period)); // data retrieved from url (string)
            DailyPrice dailyPriceBTC = DailyPrice.FromJson(json); // Deserialize the json into an object
            dailyPriceBTC.Name = ticker;
            return dailyPriceBTC;
        }
    }
}

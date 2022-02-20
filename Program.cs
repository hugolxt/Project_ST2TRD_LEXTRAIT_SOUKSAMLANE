using System;

namespace Project_ST2TRD_SOUKSAMLANE_LEXTRAIT
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Process start...");


            DailyPrice BTC = GetDailyPrice("BTC", 10);
            DailyPrice ETH = GetDailyPrice("ETH", 10);
            
            

            Datum.ShowClosingPrices(BTC);
            Datum.ShowClosingPrices(ETH);
            

            Console.WriteLine("Process ended...");
        }
        
        public static DailyPrice GetDailyPrice(string ticker, int period){
            String json = new GetDataUrl().Getdata(new GetDataUrl().UrlGenerator(ticker, period)); // data retrieved from url (string)
            DailyPrice dailyPriceBTC = DailyPrice.FromJson(json); // Deserialize the json into an object
            dailyPriceBTC.Name = ticker;
            return dailyPriceBTC;
        }
    }
    
    
}
using System;

namespace Project_ST2TRD_SOUKSAMLANE_LEXTRAIT
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Process start...");
            
            StockList stockList = new StockList(); // Global objetc
            String data = stockList.getdata(); // data retrieved from url (string)
            Console.WriteLine(data);
            stockList.deserializeStock(data); // match the json to the object created

            Console.WriteLine("Process ended...");
        }
    }
}
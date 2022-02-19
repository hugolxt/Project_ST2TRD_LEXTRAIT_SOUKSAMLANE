using System;

namespace Project_ST2TRD_SOUKSAMLANE_LEXTRAIT
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Process start...");
            
            GetDataUrl data = new GetDataUrl(); // Global objetc
            
            String json = data.getdata(); // data retrieved from url (string)
            
            var stock = Stock.FromJson(json);
            
            stock.Select(i => $"{i.Key} : [{i.Value.Eur}€ , {i.Value.Usd}$]").ToList().ForEach(Console.WriteLine);
            
            Console.WriteLine("Process ended...");
        }
    }
}
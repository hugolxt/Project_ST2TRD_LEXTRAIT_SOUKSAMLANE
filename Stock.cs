namespace Project_ST2TRD_SOUKSAMLANE_LEXTRAIT;
using System;
using System.IO;
using System.Net;
using System.Text.Json;

[Serializable]
public class StockList
{
    //public string? name { get; set; }
    //public Price? price { get; set; }
    //public DateTime date { get; set; }
    public float? USD { get; set; }
    public float? EUR { get; set; }
    //public List<Stock> stockList { get; set; }
    
    
    public void deserializeStock(String jsonString)
    {
        StockList? stockList = JsonSerializer.Deserialize<StockList>(jsonString);
        Console.WriteLine("Json input => " + jsonString);

        Console.WriteLine($"{stockList.EUR}$");
        Console.WriteLine($"{stockList.USD}€");
    }

    public string getdata()
    {
        //var url = "https://min-api.cryptocompare.com/data/price?fsym=BTC&tsyms=USD,EUR";//SINGLE STOCK
        var url = "https://min-api.cryptocompare.com/data/price?fsym=BTC&tsyms=USD,EUR";//Paste ur url here  

        WebRequest request = HttpWebRequest.Create(url);  
  
        WebResponse response = request.GetResponse();  
  
        StreamReader reader = new StreamReader(response.GetResponseStream());  
  
        string responseText = reader.ReadToEnd();
                
        //if your response is in json format just uncomment below line  

        //response.AddHeader("Content-type", "text/json");  

        //response.Write(responseText); 
        return responseText;
    }

    /*public override string ToString()
    {
        string output = "";
        if (stockList is null)
        {
             output = "not found";
        }
        else
        {
             output = "defined";
        }
        return output;
    }*/
}
public class Stock
{
    public Prices? price { get; set; }
}
    
public class Prices
{
    public float? USD { get; set; }
    public float? EUR { get; set; }
}
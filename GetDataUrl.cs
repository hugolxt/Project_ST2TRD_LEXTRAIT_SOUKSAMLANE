using System.Net;

namespace Project_ST2TRD_SOUKSAMLANE_LEXTRAIT
{
    public partial class GetDataUrl
    {
        public string UrlGenerator(string ticker, int period)
        {
            return "https://min-api.cryptocompare.com/data/v2/histoday?fsym="+ticker+"&tsym=USD&limit="+period;;
        }
        public string Getdata(string url)
        {
            //var url = "https://min-api.cryptocompare.com/data/v2/histoday?fsym=BTC&tsym=USD&limit=100";//Paste ur url here  
    
            WebRequest request = HttpWebRequest.Create(url);  
      
            WebResponse response = request.GetResponse();  
      
            StreamReader reader = new StreamReader(response.GetResponseStream());  
      
            string responseText = reader.ReadToEnd();
            
            return responseText;
        }
        
    }
}

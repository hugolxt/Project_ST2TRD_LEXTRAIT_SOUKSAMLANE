using System.Net;

namespace Project_ST2TRD_SOUKSAMLANE_LEXTRAIT
{
    [Serializable]
    public partial class GetDataUrl
    {
        public string getdata()
        {
            var url = "https://min-api.cryptocompare.com/data/pricemulti?fsyms=BTC,ETH&tsyms=USD,EUR";//Paste ur url here  
    
            WebRequest request = HttpWebRequest.Create(url);  
      
            WebResponse response = request.GetResponse();  
      
            StreamReader reader = new StreamReader(response.GetResponseStream());  
      
            string responseText = reader.ReadToEnd();
            
            return responseText;
        }
        
    }
}

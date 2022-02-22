using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;

namespace ST2TRD_Project_SOUKSAMLANE_LEXTRAIT
{
    public partial class GetDataUrl
    {  
        public string UrlGenerator(string ticker, int numberObjects)
        {
            //This function generates an URL based on a chosen ticker and a chosen number of datas
            return "https://min-api.cryptocompare.com/data/v2/histoday?fsym=" + ticker + "&tsym=USD&limit=" + numberObjects; 
        }
        public string Getdata(string url)
        {
            WebRequest request = HttpWebRequest.Create(url);

            WebResponse response = request.GetResponse();

            StreamReader reader = new StreamReader(response.GetResponseStream());

            string responseText = reader.ReadToEnd();

            return responseText;
        }
    }
}

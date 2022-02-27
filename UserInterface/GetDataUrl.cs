using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;

namespace UserInterface
{
    public partial class GetDataUrl
    {   
        //  This function generates an URL based on a chosen ticker and a chosen number of datas periods
        public string UrlGenerator(string ticker, int numberObjects)
        {
            return "https://min-api.cryptocompare.com/data/v2/histoday?fsym=" + ticker + "&tsym=USD&limit=" + numberObjects; 
        }

        //  This function create the url request and store the informations (json file {crypto object}) in a string 
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

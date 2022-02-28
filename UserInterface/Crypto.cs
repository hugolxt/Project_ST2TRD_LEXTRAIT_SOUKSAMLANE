using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Linq;

namespace UserInterface
{
    public partial class Crypto
    {
        public string? Name { get; set; }

        [JsonProperty("Response")]
        public string Response { get; set; }

        [JsonProperty("Message")]
        public string Message { get; set; }

        [JsonProperty("HasWarning")]
        public bool HasWarning { get; set; }

        [JsonProperty("Type")]
        public long Type { get; set; }

        [JsonProperty("RateLimit")]
        public RateLimit RateLimit { get; set; }

        [JsonProperty("Data")]
        public Properties Data { get; set; }
    }

    public partial class Properties
    {
        [JsonProperty("Aggregated")]
        public bool Aggregated { get; set; }

        [JsonProperty("TimeFrom")]
        public long TimeFrom { get; set; }

        [JsonProperty("TimeTo")]
        public long TimeTo { get; set; }

        [JsonProperty("Data")]
        public crypto_data[] Object { get; set; }
    }

    public partial class crypto_data
    {
        [JsonProperty("time")]
        public long Time { get; set; }

        [JsonProperty("high")]
        public double High { get; set; }

        [JsonProperty("low")]
        public double Low { get; set; }

        [JsonProperty("open")]
        public double Open { get; set; }

        [JsonProperty("volumefrom")]
        public double Volumefrom { get; set; }

        [JsonProperty("volumeto")]
        public double Volumeto { get; set; }

        [JsonProperty("close")]
        public double Close { get; set; }

        [JsonProperty("conversionType")]
        public ConversionType ConversionType { get; set; }

        [JsonProperty("conversionSymbol")]

        public string ConversionSymbol { get; set; }


        //  Converts a string format date into C# DateTime Format
        public static DateTime datetimeConvertor(string date) 
        {
            var datetime_date = Convert.ToDateTime(date);

            return datetime_date;
        }

        //  Determines if an assets price's increased or decreased on a specific period and return {Bearish, Neutral, Bullish}
        public static string growthCalculator(Crypto crypto_input, int number_days)
        {
            DateTime today = DateTime.Today;
            DateTime beginning_date = today.AddDays(-number_days);

            string strToday = today.ToString("dd/MM/yyyy");
            string strBeginningDate = beginning_date.ToString("dd/MM/yyyy");
            
            IDictionary<string, double> crypto = dictionnaryGenerator(crypto_input, "Close");

            double growth = (crypto[strBeginningDate] - crypto[strToday]);
            if (growth < 0)
            {
                return "Bearish";
            }
            else if (growth == 0)
            {
                return "Neutral";
            }
            else
            {
                return "Bullish";
            }
            return "Error";
        }

        // Create the query for specific assets, deserialize Json object into crypto object
        public static Crypto GetDailyPrice(string ticker, int period)
        {
            String json = new GetDataUrl().Getdata(new GetDataUrl().UrlGenerator(ticker, period)); // data retrieved from url (string)
            Crypto deserialized_crypto = Crypto.FromJson(json); // Deserialize the json into an object
            deserialized_crypto.Name = ticker;
            return deserialized_crypto;
        }

        // Convert a huge double variable into understanding string => {1 000 000 000 000} -> {1000B} 
        public static string FormatDoubleValues(double valueLength, double dictValue)
        {
            if (valueLength < 4)
            {
                return dictValue.ToString("#,# $", CultureInfo.InvariantCulture);
            }
            else if (valueLength < 7)
            {
                return dictValue.ToString("#,##0,K $", CultureInfo.InvariantCulture);
            }
            else if (valueLength < 10)
            {
                return dictValue.ToString("#,##0,,M $", CultureInfo.InvariantCulture);
            }
            else
            {
                return dictValue.ToString("#,##0,,B $", CultureInfo.InvariantCulture);
            }
        }


        // Calculates the total volume in dollar for a specific asset from an already set dictionary (period already given)
        public static string TotalVolumeCalculator(Crypto crypto_ticker)
        {
            IDictionary<string, double> yearlyTotalVolume = new Dictionary<string, double>();
            string stringVolume;
            double total = crypto_ticker.Data.Object[0].Volumeto;


            for (int counter = 1; counter < crypto_ticker.Data.Object.Length; counter++)
            {
                total += (crypto_ticker.Data.Object[counter-1].Volumeto);
            }
  
             double data_length = Math.Floor(Math.Log10(total) + 1);
             stringVolume = FormatDoubleValues(data_length, total);
            
            return stringVolume;
        }

        //  Creates a Dictionary based on a Crypto object
        //  Dictionary's Key = Date
        //  Dictionary's Value = Closing price or Volume (Depending on user input)
        public static IDictionary<string, double> dictionnaryGenerator(Crypto dailyPrice, string column)
        {
            IDictionary<string, double> dict = new Dictionary<string, double>();
            for (int counter = 0; counter < dailyPrice.Data.Object.Length; counter++)
            {
                if (column == "Close")
                {
                    dict.Add(UnixTimeStampToDateTime(dailyPrice.Data.Object[counter].Time), (dailyPrice.Data.Object[counter].Close));
                }
                if (column == "Volume")
                {
                    dict.Add(UnixTimeStampToDateTime(dailyPrice.Data.Object[counter].Time), (dailyPrice.Data.Object[counter].Volumeto));
                }
            }
            return dict;
        }

        //  Overloaded function by params (int numberDatas => Number Dictionary's desired datas)
        public static IDictionary<string, string> dictionnaryGenerator(Crypto dailyPrice, string column, int numberDatas)
        {
            IDictionary<string, string> dict = new Dictionary<string, string>();
            for (int counter = ((dailyPrice.Data.Object.Length) - numberDatas); counter < dailyPrice.Data.Object.Length; counter++)
            {
                double lenght = Math.Floor(Math.Log10(dailyPrice.Data.Object[counter].Volumeto) + 1);
                dict.Add(UnixTimeStampToDateTime(dailyPrice.Data.Object[counter].Time), FormatDoubleValues(lenght, dailyPrice.Data.Object[counter].Volumeto));
            }
            return dict;
        }

        //  Creates a Dictionary based on a Crypto object with <string, double> format
        public static IDictionary<string, double> dictionnaryGeneratorClose(Crypto dailyPrice, string column, int numberDatas)
        {
            IDictionary<string, double> dict = new Dictionary<string, double>();
            for (int counter = ((dailyPrice.Data.Object.Length) - numberDatas); counter < dailyPrice.Data.Object.Length; counter++)
            {
                dict.Add(UnixTimeStampToDateTime(dailyPrice.Data.Object[counter].Time), dailyPrice.Data.Object[counter].Close);
            }
            return dict;
        }
        //  Creates a Dictionary based on a Crypto object with <DateTime, double> format
        public static IDictionary<DateTime, double> dictionnaryGeneratorDate(Crypto dailyPrice, string column)
        {
            IDictionary<DateTime, double> dict = new Dictionary<DateTime, double>();
            for (int counter = 0; counter < dailyPrice.Data.Object.Length; counter++)
            {
                if (column == "Close")
                {
                    dict.Add(datetimeConvertor(UnixTimeStampToDateTime(dailyPrice.Data.Object[counter].Time)), (dailyPrice.Data.Object[counter].Close));
                }
                if (column == "Volume")
                {
                    dict.Add(datetimeConvertor(UnixTimeStampToDateTime(dailyPrice.Data.Object[counter].Time)), (dailyPrice.Data.Object[counter].Volumeto));
                }
            }
            return dict;
        }

        //  Converts a double DateTime into a formated String as {22/02/2022} 
        public static string UnixTimeStampToDateTime(double unixTimeStamp)
        {
            DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);  // Given format by CryptoApi is number of seconds from 01/01/1970
            dateTime = dateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dateTime.ToString("d");
        }

        //  Retrieves values from Dictionary into an array
        public static double[] dictValuesToArray(IDictionary<string, double> closingValues)
        {
            double[] listClosingValues = new double[closingValues.Count];
            closingValues.Values.CopyTo(listClosingValues, 0);
            var arrayClosingValues = closingValues.Values.ToArray();

            return arrayClosingValues;
        }

        //  Determines correlation between two given Crypto assets with a specific given period
        public static string correlationCalculator(Crypto firstCrypto, Crypto secondCrypto, int n)
        {
            double[] firstArray = dictValuesToArray(dictionnaryGeneratorClose(firstCrypto, "Close", n));
            double[] secondArray = dictValuesToArray(dictionnaryGeneratorClose(secondCrypto, "Close", n));

            double sumFirstArray = 0;
            double sumSecondArray = 0;
            double sumAllArray = 0;
            double squareSumFirst = 0;
            double squareSumSecond = 0;

            for (int i = 0; i < n; i++)  // Browse and prepare the data for calculation 
            {
                sumFirstArray = sumFirstArray + firstArray[i];
                sumSecondArray = sumSecondArray + secondArray[i];

                sumAllArray = sumAllArray + firstArray[i] * secondArray[i];
                squareSumFirst = squareSumFirst + firstArray[i] * firstArray[i];
                squareSumSecond = squareSumSecond + secondArray[i] * secondArray[i];
            }
            // Correlation's calculation
            float correlation = ((float)(n * sumAllArray - sumFirstArray * sumSecondArray) / (float)(Math.Sqrt((n * squareSumFirst - sumFirstArray * sumFirstArray) * (n * squareSumSecond - sumSecondArray * sumSecondArray)))) * 1000;

            return correlation.ToString("#,#%", CultureInfo.InvariantCulture);

        }
    }

    public partial class RateLimit
    {
    }

    public enum ConversionType { Direct };

    public partial class Crypto
    {
       public static Crypto FromJson(string json) => JsonConvert.DeserializeObject<Crypto>(json, Converter.Settings);
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                ConversionTypeConverter.Singleton,
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }

    internal class ConversionTypeConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(ConversionType) || t == typeof(ConversionType?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            if ((value == "direct") | (value == "multiply"))
            {
                return ConversionType.Direct;
            }
            throw new Exception("Cannot unmarshal type ConversionType");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (ConversionType)untypedValue;
            if (value == ConversionType.Direct)
            {
                serializer.Serialize(writer, "direct");
                return;
            }
            throw new Exception("Cannot marshal type ConversionType");
        }

        public static readonly ConversionTypeConverter Singleton = new ConversionTypeConverter();
    }
}

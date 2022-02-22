using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;


namespace ST2TRD_Project_SOUKSAMLANE_LEXTRAIT
{
    public partial class DailyPrice
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
        public Data Data { get; set; }
    }

    public partial class Data
    {
        [JsonProperty("Aggregated")]
        public bool Aggregated { get; set; }

        [JsonProperty("TimeFrom")]
        public long TimeFrom { get; set; }

        [JsonProperty("TimeTo")]
        public long TimeTo { get; set; }

        [JsonProperty("Data")]
        public Datum[] DataData { get; set; }
    }

    public partial class Datum
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

        public static void ShowClosingPrices(DailyPrice dailyPrice)
        {
            Console.WriteLine("[" + dailyPrice.Name + "] CLOSING PRICES");
            for (int i = 0; i < dailyPrice.Data.DataData.Length; i++)
            {
                Console.WriteLine("(" + i + ")\t[" + UnixTimeStampToDateTime(dailyPrice.Data.DataData[i].Time) + "]- " + dailyPrice.Data.DataData[i].Close + "$");
            }
        }
       
        public static void altcoinsCorrelation(DailyPrice first_dailyPrice, DailyPrice second_dailyPrice, string beginning_date, string ending_date, string column)
        {
            Console.WriteLine("\n\nCorrelation analysis of " + column + " between " + first_dailyPrice.Name + " and " + second_dailyPrice.Name + " : \n");
            IDictionary<string, double> first_altcoin = dictionnaryGenerator(first_dailyPrice,"Close");
            IDictionary<string, double> second_altcoin = dictionnaryGenerator(second_dailyPrice,"Close");
            double first_evolution = (first_altcoin[ending_date] - first_altcoin[beginning_date]);
            Console.WriteLine("From " + beginning_date + " to " + ending_date + " the " + first_dailyPrice.Name + " fluctuated of : " + first_evolution);
            double second_evolution = (second_altcoin[ending_date] - second_altcoin[beginning_date]);
            Console.WriteLine("From " + beginning_date + " to " + ending_date + " the " + second_dailyPrice.Name + " fluctuated of : " + second_evolution);
        }

        public static IDictionary<string, double> yearlyVolumeCalculator(DailyPrice dailyPrice, IDictionary<string, double> yearlyTotalVolume, string ticker)
        {
            yearlyTotalVolume[ticker] = dailyPrice.Data.DataData[0].Volumeto;

            for (int counter = 1; counter < dailyPrice.Data.DataData.Length; counter++)
            {
                yearlyTotalVolume[ticker] = (yearlyTotalVolume[ticker]) + (dailyPrice.Data.DataData[counter-1].Volumeto);
            }
            return yearlyTotalVolume;
        }

        public static IDictionary<string,double> dictionnaryGenerator(DailyPrice dailyPrice, string column)
        {
            IDictionary<string, double> dict = new Dictionary<string, double>();
            for (int counter = 0; counter < dailyPrice.Data.DataData.Length; counter++)
            {
                if (column == "Close")
                {
                    dict.Add(UnixTimeStampToDateTime(dailyPrice.Data.DataData[counter].Time), (dailyPrice.Data.DataData[counter].Close));
                }
                if (column == "Volume")
                {
                    dict.Add(UnixTimeStampToDateTime(dailyPrice.Data.DataData[counter].Time), (dailyPrice.Data.DataData[counter].Volumeto));
                }
            }
            return dict;
        }

        public static void calculateDailyMarketCap(DailyPrice dailyPrice)
        {
            Console.WriteLine("\n\nCryptocurrency [" + dailyPrice.Name + "] MARKET CAPITALIZATION\n");
            IDictionary<string, double> marketCap = new Dictionary<string, double>();

            for (int j = 0; j < dailyPrice.Data.DataData.Length; j++)
            {
                marketCap.Add(UnixTimeStampToDateTime(dailyPrice.Data.DataData[j].Time), (dailyPrice.Data.DataData[j].Volumeto));          
            }
            displayDictionnary(marketCap, "Day", "Market Capitalization");
        }

        public static void displayDictionnary(IDictionary<string,double> Dict, string key, string value_type)
        {
            foreach (KeyValuePair<string, double> kvp in Dict)
                Console.WriteLine(key + " : {0} ||| "+ value_type +" : {1}", kvp.Key, kvp.Value);
        }

        public static string UnixTimeStampToDateTime(double unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dateTime = dateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dateTime.ToString("d");
        }
    }

    public partial class RateLimit
    {
    }

    public enum ConversionType { Direct };

    public partial class DailyPrice
    {
       public static DailyPrice FromJson(string json) => JsonConvert.DeserializeObject<DailyPrice>(json, Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this DailyPrice self) => JsonConvert.SerializeObject(self, Converter.Settings);
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

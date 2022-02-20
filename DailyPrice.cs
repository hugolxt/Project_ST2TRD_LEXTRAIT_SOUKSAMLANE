namespace Project_ST2TRD_SOUKSAMLANE_LEXTRAIT
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

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
            Console.WriteLine("["+dailyPrice.Name+"] CLOSING PRICES");
            for (int i = 0; i < dailyPrice.Data.DataData.Length; i++)
            {
                Console.WriteLine("("+i+")\t["+UnixTimeStampToDateTime(dailyPrice.Data.DataData[i].Time)+"]- "+dailyPrice.Data.DataData[i].Close+"$");
            }
        }
        public static string UnixTimeStampToDateTime( double unixTimeStamp )
        {
            // Unix timestamp is seconds past epoch
            DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dateTime = dateTime.AddSeconds( unixTimeStamp ).ToLocalTime();
            return dateTime.ToString("d");
        }
    }

    public partial class RateLimit
    {
    }
    
    

    public enum ConversionType { Direct };

    public partial class DailyPrice
    {
        public static DailyPrice FromJson(string json) => JsonConvert.DeserializeObject<DailyPrice>(json, Project_ST2TRD_SOUKSAMLANE_LEXTRAIT.Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this DailyPrice self) => JsonConvert.SerializeObject(self, Project_ST2TRD_SOUKSAMLANE_LEXTRAIT.Converter.Settings);
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
            if (value == "direct")
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

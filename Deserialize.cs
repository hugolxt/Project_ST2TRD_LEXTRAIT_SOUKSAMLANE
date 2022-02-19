namespace Project_ST2TRD_SOUKSAMLANE_LEXTRAIT
{
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class Stock
    {
        [JsonProperty("USD")]
        public double Usd { get; set; }

        [JsonProperty("EUR")]
        public double Eur { get; set; }
        
        
    }

    public partial class Stock
    {
        public static Dictionary<string, Stock> FromJson(string json) => JsonConvert.DeserializeObject<Dictionary<string, Stock>>(json, Converter.Settings);
        
    }

    public static class Serialize
    {
        public static string ToJson(this Dictionary<string, Stock> self) => JsonConvert.SerializeObject(self, Converter.Settings);
        
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }
}
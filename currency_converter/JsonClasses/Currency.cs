using System.Text.Json.Serialization;
using currency_converter.JsonClasses;

namespace currency_converter
{
    public class Currency
    {
        [JsonPropertyName("meta")]
        public MetaData Meta { get; set; }

        [JsonPropertyName("data")]
        public Dictionary<string, CurrencyInfo> Data { get; set; }
    }
}

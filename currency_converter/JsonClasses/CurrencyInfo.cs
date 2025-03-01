using System.Text.Json.Serialization;

namespace currency_converter.JsonClasses
{
    public class CurrencyInfo
    {
        [JsonPropertyName("code")]
        public string Code { get; set; }

        [JsonPropertyName("value")]
        public float Value { get; set; }
    }
}

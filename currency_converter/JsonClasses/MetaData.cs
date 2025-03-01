using System.Text.Json.Serialization;

namespace currency_converter.JsonClasses
{
    public class MetaData
    {
        [JsonPropertyName("last_updated_at")]
        public string LastUpdateAt { get; set; }
    }
}

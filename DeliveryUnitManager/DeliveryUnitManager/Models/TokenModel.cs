using System.Text.Json.Serialization;

namespace DeliveryUnitManager.Models
{
    public class TokenModel
    {
        public bool result { get; set; }
        public string Message { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Token { get; set; }
    }
}

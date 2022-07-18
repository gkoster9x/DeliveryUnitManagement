using System.Text.Json.Serialization;

namespace DeliveryUnitManager.Models
{
    public class TokenModel
    {
        public bool result { get; set; }
        public string Message { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Token { get; set; }

        public TokenModel(bool result, string message, string? token = null)
        {
            this.result=result;
            Message=message;
            Token=token;
        }
    }
}

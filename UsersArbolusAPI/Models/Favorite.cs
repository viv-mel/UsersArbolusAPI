using System.Text.Json.Serialization;

namespace UsersArbolusAPI.Models
{
    public class Favorite
    {
        [JsonPropertyName("color")]
        public string Color { get; set; }

        [JsonPropertyName("food")]
        public string Food { get; set; }

        [JsonPropertyName("random_string")]
        public string RandomString { get; set; }

        [JsonPropertyName("song")]
        public string Song { get; set; }
    }
}

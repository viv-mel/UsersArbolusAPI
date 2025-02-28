
using System.Text.Json.Serialization;

namespace UsersArbolusAPI.Models
{
    public record UserList
    {
        [JsonPropertyName("current")]
        public int Current { get; set; }

        [JsonPropertyName("total")]
        public int Total { get; set; }

        [JsonPropertyName("results")]
        public List<User> Results { get; set; } = [];
    }
}

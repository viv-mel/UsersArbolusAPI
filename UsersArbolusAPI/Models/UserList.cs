
using System.Text.Json.Serialization;

namespace UsersArbolusAPI.Models
{
    public class UserList
    {
        [JsonPropertyName("current")]
        public int Current { get; set; }

        [JsonPropertyName("total")]
        public int Total { get; set; }

        [JsonPropertyName("results")]
        public List<User> Results { get; set; }
    }
}

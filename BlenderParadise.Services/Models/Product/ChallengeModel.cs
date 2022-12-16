using System.Text.Json.Serialization;

namespace BlenderParadise.Models.Product
{
    public class ChallengeModel
    {
        [JsonPropertyName("hobby")]
        public string Hobby { get; set; } = null!;

        [JsonPropertyName("link")]
        public string Link { get; set; } = null!;

        [JsonPropertyName("category")]
        public string Category { get; set; } = null!;
    }
}

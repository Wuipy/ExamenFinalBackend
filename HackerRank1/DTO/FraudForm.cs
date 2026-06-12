using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace LibraryService.WebAPI.DTO
{
    public class FraudForm
    {
        [Required]
        [JsonProperty("impostorDetails")]
        public string ImpostorDetails { get; set; }

        [Required]
        [JsonProperty("contactInfo")]
        public string ContactInfo { get; set; }

        [Required]
        [JsonProperty("comments")]
        public string Comments { get; set; }
    }
}

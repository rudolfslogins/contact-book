using Newtonsoft.Json;

namespace contact_book.Models
{
    public class EntityApiModel
    {
        [JsonProperty(Order = 5)]
        public int Id { get; set; }
    }
}
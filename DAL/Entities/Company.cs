using System.Text.Json.Serialization;
using DAL.Entities.Enums;
using DAL.Helpers;

namespace DAL.Entities
{
    public class Company : BaseEntity<int>
    {
        public string Title { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public TypeOfBusiness TypeOfBusiness { get; set; }
    }
}
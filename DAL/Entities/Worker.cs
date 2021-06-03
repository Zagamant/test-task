using System;
using System.Text.Json.Serialization;
using DAL.Entities.Enums;
using DAL.Helpers;

namespace DAL.Entities
{
    public class Worker : BaseEntity<int>
    {
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Patronymic { get; set; }
        public DateTime EmploymentDate { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public Position Position { get; set; }

        public int CompanyId { get; set; }
        public Company Company { get; set; }
    }
}
using DAL.Entities.Enums;
using DAL.Helpers;

namespace DAL.Entities
{

    public class Company : BaseEntity<int>
    {
        public string Title { get; set; }

        public int Size { get; set; }
        public TypeOfBusiness TypeOfBusiness { get; set; }
    }
}
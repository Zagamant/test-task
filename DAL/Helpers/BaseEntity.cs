namespace DAL.Helpers
{
    public abstract class BaseEntity<TId>
    {
        public TId Id { get; set; }
    }
}
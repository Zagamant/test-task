namespace DAL.Helpers
{
    /// <summary>
    /// Base class to define type of id
    /// </summary>
    /// <typeparam name="TId">Type of id</typeparam>
    public abstract class BaseEntity<TId>
    {
        public TId Id { get; set; }
    }
}
namespace BLL.Helpers
{
    public abstract class MapperBase<TSource, TDest>
    {
        public abstract TSource Map(TDest element);
        public abstract TDest Map(TSource element);
    }
}
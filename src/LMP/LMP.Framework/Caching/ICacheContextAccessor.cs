namespace LMP.Caching
{
    public interface ICacheContextAccessor {
        IAcquireContext Current { get; set; }
    }
}
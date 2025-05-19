namespace Services.Abstractions
{
    public interface ICacheService
    {
        Task<string?> GetAsync(string cachekey);
        Task SetAsync(string cachekey, object value,TimeSpan timeToLive);
    }
}

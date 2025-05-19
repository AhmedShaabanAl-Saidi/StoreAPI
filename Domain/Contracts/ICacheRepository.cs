namespace Domain.Contracts
{
    public interface ICacheRepository
    {
        Task<string> GetAsync<T>(string cacheKey);
        Task SetAsync(string cacheKey, string value, TimeSpan timeToLive);
    }
}

namespace Challenge.Infrastructure.Extensions;

public static class CacheExtensions
{
    public static string GenerateCacheKey<T>(string type, string value)
    {
        string entityType = typeof(T).Name;
        
        return $"{entityType}_{type}_{value}";
    }
}

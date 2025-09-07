using Domain.Interfaces;
using Domain.ValueObjects;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace Application.Services.Cache;

internal sealed class UserCacheService : IUserCacheService
{
  private static string BaseCacheKey = "EntityCache"; 
  private readonly IDistributedCache cache;
  private readonly TimeSpan defaultCachingDuration;

  public UserCacheService(
    IDistributedCache cache,
    IOptions<CacheOptions> cacheOptions)
  {
    this.cache = cache;
    defaultCachingDuration = cacheOptions.Value.DefaultCacheDuration;
  }
    
  public async Task<bool?> Get(
    string password,
    string name,
    CancellationToken cancellationToken)
  {
    var key = GetKey(password + name);
    
    var cachedValue = await cache.GetStringAsync(key, cancellationToken);
    
    if (cachedValue == null)
      return null;
    
    if (bool.TryParse(cachedValue, out bool value))
      return value;
    
    await cache.RemoveAsync(key, cancellationToken);
    return null;
  }
  
  public Task Set(
    string password,
    string name,
    bool value, 
    CancellationToken cancellationToken)
  {
    var key = GetKey(password + name);
      
    return cache.SetStringAsync(key,
      value.ToString(),
      new DistributedCacheEntryOptions
      {
        AbsoluteExpirationRelativeToNow = defaultCachingDuration,
      },
      cancellationToken);
  }

  public Task Delete(
    string password,
    string name,
    CancellationToken cancellationToken)
  {
    var key = GetKey(password + name);
        
    return cache.RemoveAsync(key, cancellationToken); 
  }

  private string GetKey(string entityKey)
  {
    return BaseCacheKey + "\\" + entityKey;
  }
}
namespace Application.Services.Cache;

public sealed class CacheOptions
{
  public static string OptionsKey { get; } = "CacheSettings";
  
  public TimeSpan DefaultCacheDuration { get; set; } = TimeSpan.FromMinutes(30);
}
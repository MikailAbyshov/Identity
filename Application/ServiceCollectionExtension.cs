using Application.Services.Cache;
using Application.Services.Users;
using Microsoft.Extensions.DependencyInjection;

namespace Application;
  
/// <summary>
/// Добавление сервисов в DI
/// </summary>
public static class ServiceCollectionExtension
{
  public static void AddServices(this IServiceCollection services)
  {
    services.AddTransient<IUserService, UserService>();
    services.AddTransient<IUserCacheService, UserCacheService>();
  }
}
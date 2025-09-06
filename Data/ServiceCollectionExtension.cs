using Application.Interacting;
using Data.Engine;
using Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Data;
  
/// <summary>
/// Добавление DAL в DI
/// </summary>
public static class ServiceCollectionExtension
{
  public static void AddDataAccessLayer(this IServiceCollection services, IConfiguration configuration)
  {
    services.AddDbContext<IIdentityContext, IdentityContext>(options => 
      options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));
    
    services.AddTransient<IUserRepository, UserRepository>();
  }
}
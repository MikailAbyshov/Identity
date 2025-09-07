using Application;
using Application.Services.Cache;
using Application.Services.Tokens;
using Data;
using Web;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDataAccessLayer(builder.Configuration);
builder.Services.Configure<CacheOptions>(builder.Configuration.GetSection(CacheOptions.OptionsKey));
builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection(JwtOptions.OptionsKey));
builder.Services.AddApiAuthentification(builder.Configuration);
builder.Services.AddSwaggerWithAuth();
builder.Services.AddServices();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddStackExchangeRedisCache(options
  => options.Configuration = builder.Configuration.GetConnectionString("Cache"));
builder.Services.AddControllers();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
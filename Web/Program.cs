using Application;
using Application.Services.Cache;
using Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDataAccessLayer(builder.Configuration);
builder.Services.Configure<CacheOptions>(builder.Configuration.GetSection(CacheOptions.OptionsKey));
builder.Services.AddServices();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddStackExchangeRedisCache(options
  => options.Configuration = builder.Configuration.GetConnectionString("Cache"));
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
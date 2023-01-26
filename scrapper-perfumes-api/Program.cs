using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using scrapper_perfumes_yves_common;
using scrapper_perfumes_yves_common.Interfaces;
using scrapper_perfumes_yves_common.Repository;
using scrapper_perfumes_yves_common.ServiceConfiguration;
using scrapper_perfumes_yves_common.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

IConfiguration configuration = builder.Configuration;
builder.Services.Configure<Configuration>(configuration.GetSection(nameof(Configuration)));

builder.Services.AddOutputCache();

builder.Services.AddTransient<IScrapperService, ScrapperService>();
builder.Services.AddTransient<IProductService, ProductService>();
builder.Services.AddTransient<IProductRepository, ProductRepository>();

builder.Services.AddDbContext<DataContext>();


var app = builder.Build();
app.UseOutputCache();
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

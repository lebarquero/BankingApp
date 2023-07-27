using BankingAPI;
using BankingAPI.Business;
using BankingAPI.Business.IServices;
using BankingAPI.DataAccess;
using BankingAPI.DataAccess.Repositories;
using BankingAPI.DataAccess.Repositories.IRepositories;
using BankingAPI.Entities;
using BankingAPI.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<BankingDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("BankingConnection"));
});

builder.Services.AddAutoMapper(typeof(MappingConfig));

builder.Services.AddScoped<IRepository<Cliente>, Repository<Cliente>>();
builder.Services.AddScoped<IRepository<Cuenta>, Repository<Cuenta>>();
builder.Services.AddScoped<IRepository<Movimiento>, Repository<Movimiento>>();
builder.Services.AddScoped<IService<Movimiento>, MovimientoService>();

builder.Services.AddControllers()
    .AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()))
    .AddNewtonsoftJson();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// Custom Middleware to handle exceptions and logging
app.UseMiddleware<ErrorHandlerMiddleware>();

app.Run();

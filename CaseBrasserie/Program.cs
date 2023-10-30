using CaseBrasserie.Application.Repositories;
using CaseBrasserie.Application.Repositories.Implementations;
using CaseBrasserie.Application.Repositories.Interfaces;
using CaseBrasserie.Infrastructure.Data.DbContexts;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddNewtonsoftJson(
     options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

builder.Services.AddDbContext<BrasserieContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Brasserie")));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IBrasserieContext, BrasserieContext>();
builder.Services.AddScoped<IBiereRepository, BiereRepository>();
builder.Services.AddScoped<IBrasserieRepository, BrasserieRepository>();
builder.Services.AddScoped<IGrossisteRepository, GrossisteRepository>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

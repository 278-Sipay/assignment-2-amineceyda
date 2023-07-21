using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using AutoMapper;
using Assigment2Api.SchemaOperations.MapperConfig;
using Assigment2Api.DBOperations.DBContext;
using Assigment2Api.DBOperations.Repository;
using Assigment2Api.SchemaOperations.Transaction;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Register AutoMapper
var mapperConfig = new MapperConfiguration(config =>
{
    config.AddProfile<MapperConfig>();
});

builder.Services.AddSingleton<IMapper>(mapperConfig.CreateMapper());

// Add the database context 
var dbType = builder.Configuration.GetConnectionString("DbType");
if (dbType == "Sql")
{
    var dbConfig = builder.Configuration.GetConnectionString("MsSqlConnection");
    builder.Services.AddDbContext<SimDbContext>(opts =>
        opts.UseSqlServer(dbConfig));
}
else if (dbType == "PostgreSql")
{
    var dbConfig = builder.Configuration.GetConnectionString("PostgreSqlConnection");
    builder.Services.AddDbContext<SimDbContext>(opts =>
        opts.UseNpgsql(dbConfig));
}

// Register the repositories and services

builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Assigment2Api API", Version = "v1" });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Assigment2Api API v1");
    c.RoutePrefix = string.Empty;
});

app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.Run();

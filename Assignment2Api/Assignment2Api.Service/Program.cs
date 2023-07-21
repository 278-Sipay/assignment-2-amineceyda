using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore; 

using Assignment2Api.Data;
using Assignment2Api.Base;
using Assignment2Api.Schema;
using Assignment2Api.Data.DBContext;

namespace Assignment2Api.Service
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Assigment2 Api Collection", Version = "v1" });
            });

            // Database injection
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

            // Add other services as needed

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Assigment2Api v1"));
            }

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}

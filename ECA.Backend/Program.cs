using ECA.Backend.Common.Options;
using ECA.Backend.Data.Context;
using ECA.Backend.Logic.Interfaces;
using ECA.Backend.Logic.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Npgsql;

namespace ECA.Backend
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("host.settings.json", optional: true, reloadOnChange: false)
                .AddJsonFile("local.settings.json", optional: true, reloadOnChange: false)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: false)
                .AddEnvironmentVariables()
                .Build();




            var connectionString = new NpgsqlConnectionStringBuilder();
            connectionString.Host = configuration["PostgreSql:Host"];
            connectionString.Port = int.Parse(configuration["PostgreSql:Port"]);
            connectionString.Database = configuration["PostgreSql:Database"];
            connectionString.Username = configuration["PostgreSql:Username"];
            connectionString.Password = configuration["PostgreSql:DbPassword"];
            

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // DI
            builder.Services
                .AddLogging()
                .AddScoped<IPasswordService, PasswordService>(provider =>
                {
                    var options = new HashingOptions() { Iterations = 10000 };
                    var passwordService = new PasswordService(options);
                    return passwordService;
                })
                .AddScoped<IUserService, UserService>()
                .AddDbContextFactory<BackendContext>(opt => opt.UseNpgsql(connectionString.ConnectionString))
                .AddDbContext<DbContext, BackendContext>(opt => opt.UseNpgsql(connectionString.ConnectionString));



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
        }
    }
}
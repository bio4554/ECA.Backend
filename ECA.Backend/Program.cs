using ECA.Backend.Common.Models.Context;
using ECA.Backend.Common.Options;
using ECA.Backend.Logic.Interfaces;
using ECA.Backend.Logic.Services;
using Microsoft.EntityFrameworkCore;

namespace ECA.Backend
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
                .AddDbContext<UsersContext>(opt => opt.UseInMemoryDatabase("TodoList"));



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
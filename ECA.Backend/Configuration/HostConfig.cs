using ECA.Backend.Common.Options;
using ECA.Backend.Logic.Interfaces;
using ECA.Backend.Logic.Services;

namespace ECA.Backend.Configuration
{
    public static class HostConfig
    {
        public static async Task<IHostBuilder> HostConfiguration(this IHostBuilder builder)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("host.settings.json", optional: true, reloadOnChange: false)
                .AddJsonFile("local.settings.json", optional: true, reloadOnChange: false)
                .AddEnvironmentVariables()
                .Build();

            var config = new Config
            {
                Database = configuration["Database"]
            };

            builder
                .ConfigureAppConfiguration(configurationBuilder =>
                {
                    configurationBuilder.AddEnvironmentVariables();
                    var config = configurationBuilder.Build();
                })
                .ConfigureServices(services =>
                {
                    services
                        .AddLogging()
                        .AddOptions()
                        .AddSingleton<IConfig>(config)
                        .AddScoped<IPasswordService>(provider =>
                        {
                            var options = new HashingOptions() { Iterations = 10000 };
                            var passwordService = new PasswordService(options);
                            return passwordService;
                        })
                        .AddSingleton<IUserService, UserService>();
                });

            return builder;
        }
    }
}

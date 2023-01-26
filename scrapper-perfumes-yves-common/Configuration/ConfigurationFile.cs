using Microsoft.Extensions.Configuration;

namespace scrapper_perfumes_yves_common.Configuration
{
    public static class ConfigurationFile
    {
        public static Config GetConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("config.json", optional: false);

            IConfiguration config = builder.Build();

            var configuration = config.GetSection("Config").Get<Config>();

            return configuration;
        }
    }
}

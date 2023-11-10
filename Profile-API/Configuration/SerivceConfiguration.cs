using Microsoft.Extensions.Primitives;

namespace Profile_API.Configuration
{
    public class SerivceConfiguration : IConfiguration
    {
        private readonly IConfigurationRoot _configurationRoot;

        public SerivceConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            _configurationRoot = builder.Build();
        }

        public string this[string key]
        {
            get => _configurationRoot[key];
            set => _configurationRoot[key] = value;
        }

        public IConfigurationSection GetSection(string key)
        {
            return _configurationRoot.GetSection(key);
        }

        public IEnumerable<IConfigurationSection> GetChildren()
        {
            return _configurationRoot.GetChildren();
        }

        public IChangeToken GetReloadToken()
        {
            return _configurationRoot.GetReloadToken();
        }
    }
}

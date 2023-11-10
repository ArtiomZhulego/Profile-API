using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Persistance.Migration;
using Profile_API;
using System.Data;

namespace Factories
{
    public class WebAppFactory<TEntryPoint> : WebApplicationFactory<Program> where TEntryPoint : Program
    {
        private IDbConnection _connection;
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                services.AddScoped<IDbConnection>(provider =>
                {
                    if (_connection == null)
                    {
                        _connection = new InMemoryDatabase().OpenConnection();
                        _connection.Open();
                    }

                    return _connection;
                });
            });
        }
    }
}

using Dapper;

namespace Persistance.Migration
{
    public class Database
    {
        private readonly DapperContext _context;
        public Database(DapperContext context)
        {
            _context = context;
        }
        public void CreateDatabase()
        {
            var query = "SELECT * FROM pg_database WHERE datname = @DatabaseName";

            using (var connection = _context.CreateConnection())
            {
                var records = connection.Query(query, new { DatabaseName = "profile_api" });

                if (!records.Any())
                    connection.Execute($"CREATE DATABASE profile_api");
                    
            }
        }
    }
}

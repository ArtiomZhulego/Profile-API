using ServiceStack.OrmLite;
using System.Data;

namespace Persistance.Migration
{
    public class InMemoryDatabase
    {
        private readonly OrmLiteConnectionFactory dbFactory =
            new OrmLiteConnectionFactory("Server=(localdb)\\mssqllocaldb;Trusted_Connection=True;", PostgreSqlDialect.Instance);

        public IDbConnection OpenConnection() => dbFactory.OpenDbConnection();
    }
}

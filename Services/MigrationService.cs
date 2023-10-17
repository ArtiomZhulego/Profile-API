using Microsoft.Extensions.Configuration;
using Npgsql;
using Dapper;

namespace Services
{
    public class MigrationService
    {
        private NpgsqlConnection connection;

        public MigrationService(IConfiguration configuration)
        {
            connection = new NpgsqlConnection(configuration.GetConnectionString("DefaultConnection"));
        }

        public async void Migrate()
        {
            connection.Open();

            connection.Execute("CREATE TABLE IF NOT EXISTS public.\"Doctor\" ( \"Id\" uuid NOT NULL," +
                               "\"Photo\" \"varchar\", " +
                               "\"FirstName\" \"varchar\" NOT NULL," +
                               "\"MiddleName\" \"varchar\" NOT NULL," +
                               "\"LastName\" \"varchar\"," +
                               "\"DateOfBirth\" date NOT NULL," +
                               "\"Email\" \"varchar\"," +
                               "\"SpecializationId\" uuid NOT NULL," +
                               "\"OfficeId\" uuid NOT NULL," +
                               "\"CareerStartYear\" date NOT NULL," +
                               "\"DoctorStatuses\" integer NOT NULL," +
                               "CONSTRAINT doctor_pkey PRIMARY KEY (\"Id\")\r\n)");

            connection.Execute("CREATE TABLE IF NOT EXISTS public.\"Patient\"" +
                               "(\"Id\" uuid  NOT NULL," +
                               " \"FirstName\" \"varchar\" NOT NULL," +
                               " \"MiddleName\" \"varchar\", " +
                               " \"Email\" \"varchar\", " +
                               " \"LastName\" \"varchar\" NOT NULL," +
                               " \"Photo\" \"varchar\", " +
                               " \"PhoneNumber\" bigint, " +
                               " \"AccountId\" uuid, " +
                               " \"DateOfBirth\" date NOT NULL )");
            connection.Execute("CREATE TABLE IF NOT EXISTS public.\"Receptionist\" (\"Id\" uuid NOT NULL," +
                               " \"FirstName\" \"varchar\" NOT NULL," +
                               " \"LastName\" \"varchar\"," +
                               " \"MiddleName\" \"varchar\"," +
                               " \"Email\" \"varchar\", " +
                               " \"OfficeId\" uuid NOT NULL, " +
                               " \"Photo\" \"varchar\")");

            connection.Close();
        }
    }
}

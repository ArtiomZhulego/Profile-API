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
                               "\"Photo\" \"char\"[], " +
                               "\"FirstName\" \"char\"[] NOT NULL," +
                               "\"MiddleName\" \"char\"[] NOT NULL," +
                               "\"LastName\" \"char\"[]," +
                               "\"DateOfBirth\" date NOT NULL," +
                               "\"Email\" \"char\"[]," +
                               "\"SpecializationId\" uuid NOT NULL," +
                               "\"OfficeId\" uuid NOT NULL," +
                               "\"CareerStartYear\" date NOT NULL," +
                               "\"DoctorStatuses\" integer NOT NULL," +
                               "CONSTRAINT doctor_pkey PRIMARY KEY (\"Id\")\r\n)");

            connection.Execute("CREATE TABLE IF NOT EXISTS public.\"Patient\"" +
                               "(\"Id\" uuid NOT NULL," +
                               " \"FirstName\" \"char\"[] NOT NULL," +
                               " \"MiddleName\" \"char\"[], " +
                               " \"LastName\" \"char\"[] NOT NULL," +
                               " \"Photo\" \"char\"[], " +
                               " \"PhoneNumber\" bigint, " +
                               " \"DateOfBirth\" date NOT NULL )");
            connection.Execute("CREATE TABLE IF NOT EXISTS public.\"Receptionist\" (\"Id\" uuid NOT NULL," +
                               " \"FirstName\" \"char\"[] NOT NULL," +
                               " \"LastName\" \"char\"[]," +
                               " \"MiddleName\" \"char\"[]," +
                               " \"Email\" \"char\"[], " +
                               " \"OfficeId\" uuid NOT NULL, " +
                               " \"Photo\" \"char\"[])");

            connection.Close();
        }
    }
}

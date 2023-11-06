using Dapper;
using Domain;
using Domain.Repositories;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace Persistance
{
    public class PatientRepository : IPatientRepository
    {
        private NpgsqlConnection connection;

        public PatientRepository(IConfiguration configuration) 
        {
            connection = new NpgsqlConnection(configuration.GetConnectionString("DefaultConnection"));
        }

        public async Task<Patient> CreateAsync(Patient _patient, CancellationToken token)
        {
            if (connection.State == System.Data.ConnectionState.Closed)
                connection.Open(); 
            
            await connection.QueryAsync<Patient>($"INSERT INTO public.\"Patient\" (\"Id\",\"FirstName\", \"MiddleName\", \"LastName\", \"Photo\", \"PhoneNumber\", \"DateOfBirth\",\"AccountId\",\"Email\") " +
                                                                         $"VALUES (@Id,@FirstName,@MiddleName,@LastName,@Photo,@PhoneNumber,@DateOfBirth,@AccountId,@Email)",_patient);


            connection.Close();

            return _patient;
        }

        public async Task DeleteAsync(Guid patientId, CancellationToken token)
        {
            if (connection.State == System.Data.ConnectionState.Closed)            
                connection.Open();  

            await connection.QueryAsync($"DELETE FROM public.\"Patient\" WHERE \"Id\" = @Id", new { Id = patientId });

            connection.Close();
        }

        public async Task<List<Patient>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            if (connection.State == System.Data.ConnectionState.Closed)
                connection.Open();
            
            var patients = (List<Patient>)await connection.QueryAsync<Patient>("SELECT * FROM public.\"Patient\" ORDER BY \"Id\" ASC\r\n");

            connection.Close();

            return patients;
        }

        public async Task<Patient> GetByIdAsync(Guid patientId, CancellationToken cancellationToken = default)
        {
            if (connection.State == System.Data.ConnectionState.Closed)
                connection.Open();

            var patient = await connection.QueryAsync<Patient>($"SELECT * FROM public.\"Patient\" WHERE \"Id\" = @Id",new { Id = patientId});

            connection.Close();

            return patient.FirstOrDefault();
        }

        public async Task<List<Patient>> SearchByNameAsync(string fullName, CancellationToken token)
        {
            if (connection.State == System.Data.ConnectionState.Closed)
                connection.Open();

            var patientsList = (List<Patient>) await connection.QueryAsync<Patient>($"Select * FROM public.\"Patient\" " +
                                                                                    $"WHERE \"FirstName\" LIKE @FirstName OR " +
                                                                                    $"\"MiddleName\" LIKE @MiddleName OR" +
                                                                                    $"\"LastName\" LIKE @LastName",new { FirstName = fullName, MiddleName = fullName, LastName = fullName});

            connection.Close();

            return patientsList;
        }

        public async Task<Patient> UpdateAsync(Guid patientId, Patient newPatient, CancellationToken token)
        {
            if (connection.State == System.Data.ConnectionState.Closed)
                connection.Open();

            var patient = (Patient) await connection.QueryAsync<Patient>($"UPDATE Patient SET \"FirstName\" = @FirstName" +
                                                                         $"AND \"MiddleName\" = @MiddleName" +
                                                                         $"AND \"LastName\" = @LastName" +
                                                                         $"AND \"Photo\" = @Photo" +
                                                                         $"AND \"PhoneNumber\" = @PhoneNumber" +
                                                                         $"AND \"DateOfBirth\" = @DateOfBirth" +
                                                                         $"AND \"AccountId\" = @AccountId" +
                                                                         $"AND \"Email\" = @Email" +
                                                                         $"WHERE \"Id\" = @Id", newPatient);

            connection.Close();

            return patient;
        }
    }
}

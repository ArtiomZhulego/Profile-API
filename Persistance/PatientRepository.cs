using Dapper;
using Domain;
using Domain.Repositories;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace Persistance
{
    public class PatientRepository : IPatientRepository
    {
        private readonly NpgsqlConnection _connection;

        public PatientRepository(IConfiguration configuration) 
        {
            _connection = new NpgsqlConnection(configuration.GetConnectionString("DefaultConnection"));
        }

        public async Task<Patient> CreateAsync(Patient patient, CancellationToken token)
        {
            await _connection.QueryAsync<Patient>($"INSERT INTO public.\"Patient\" (\"Id\",\"FirstName\", \"MiddleName\", \"LastName\", \"Photo\", \"PhoneNumber\", \"DateOfBirth\",\"AccountId\",\"Email\") " +
                                                 $"VALUES (@Id,@FirstName,@MiddleName,@LastName,@Photo,@PhoneNumber,@DateOfBirth,@AccountId,@Email)",patient);

            return patient;
        }

        public async Task DeleteAsync(Guid patientId, CancellationToken token) =>
            await _connection.QueryAsync($"DELETE FROM public.\"Patient\" WHERE \"Id\" = @Id", new { Id = patientId });
        
        public async Task<IEnumerable<Patient>> GetAllAsync(CancellationToken cancellationToken = default) =>
            await _connection.QueryAsync<Patient>("SELECT * FROM public.\"Patient\" ORDER BY \"Id\" ASC\r\n");

        public async Task<Patient> GetByIdAsync(Guid patientId, CancellationToken cancellationToken = default)
        {
            var patient = await _connection.QueryAsync<Patient>($"SELECT * FROM public.\"Patient\" WHERE \"Id\" = @Id",new { Id = patientId});

            return patient.FirstOrDefault();
        }

        public async Task<IEnumerable<Patient>> SearchByNameAsync(string fullName, CancellationToken token) =>
            await _connection.QueryAsync<Patient>($"Select * FROM public.\"Patient\" " +
                                                                                    $"WHERE \"FirstName\" LIKE @FirstName OR " +
                                                                                    $"\"MiddleName\" LIKE @MiddleName OR" +
                                                                                    $"\"LastName\" LIKE @LastName",new { FirstName = fullName, MiddleName = fullName, LastName = fullName});

        public async Task<Patient> UpdateAsync(Guid patientId, Patient newPatient, CancellationToken token)
        {
            var patient = await _connection.QueryAsync<Patient>($"UPDATE public.\"Patient\" SET \"FirstName\" = @FirstName, " +
                                                                         $"\"MiddleName\" = @MiddleName, " +
                                                                         $"\"LastName\" = @LastName, " +
                                                                         $"\"Photo\" = @Photo, " +
                                                                         $"\"PhoneNumber\" = @PhoneNumber, " +
                                                                         $"\"DateOfBirth\" = @DateOfBirth, " +
                                                                         $"\"AccountId\" = @AccountId, " +
                                                                         $"\"Email\" = @Email " +
                                                                         $"WHERE \"Id\" = @Id ", newPatient);

            return patient.FirstOrDefault();
        }
    }
}

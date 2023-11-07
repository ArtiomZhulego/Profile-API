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
            await connection.QueryAsync<Patient>($"INSERT INTO public.\"Patient\" (\"Id\",\"FirstName\", \"MiddleName\", \"LastName\", \"Photo\", \"PhoneNumber\", \"DateOfBirth\",\"AccountId\",\"Email\") " +
                                                                         $"VALUES (@Id,@FirstName,@MiddleName,@LastName,@Photo,@PhoneNumber,@DateOfBirth,@AccountId,@Email)",_patient);

            return _patient;
        }

        public async Task DeleteAsync(Guid patientId, CancellationToken token)
        {
            await connection.QueryAsync($"DELETE FROM public.\"Patient\" WHERE \"Id\" = @Id", new { Id = patientId });
        }

        public async Task<List<Patient>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var patients = (List<Patient>)await connection.QueryAsync<Patient>("SELECT * FROM public.\"Patient\" ORDER BY \"Id\" ASC\r\n");

            return patients;
        }

        public async Task<Patient> GetByIdAsync(Guid patientId, CancellationToken cancellationToken = default)
        {
            var patient = await connection.QueryAsync<Patient>($"SELECT * FROM public.\"Patient\" WHERE \"Id\" = @Id",new { Id = patientId});

            return patient.FirstOrDefault();
        }

        public async Task<List<Patient>> SearchByNameAsync(string fullName, CancellationToken token)
        {
            var patientsList = (List<Patient>) await connection.QueryAsync<Patient>($"Select * FROM public.\"Patient\" " +
                                                                                    $"WHERE \"FirstName\" LIKE @FirstName OR " +
                                                                                    $"\"MiddleName\" LIKE @MiddleName OR" +
                                                                                    $"\"LastName\" LIKE @LastName",new { FirstName = fullName, MiddleName = fullName, LastName = fullName});

            return patientsList;
        }

        public async Task<Patient> UpdateAsync(Guid patientId, Patient newPatient, CancellationToken token)
        {
            var patient = (Patient) await connection.QueryAsync<Patient>($"UPDATE Patient SET \"FirstName\" = @FirstName" +
                                                                         $"AND \"MiddleName\" = @MiddleName" +
                                                                         $"AND \"LastName\" = @LastName" +
                                                                         $"AND \"Photo\" = @Photo" +
                                                                         $"AND \"PhoneNumber\" = @PhoneNumber" +
                                                                         $"AND \"DateOfBirth\" = @DateOfBirth" +
                                                                         $"AND \"AccountId\" = @AccountId" +
                                                                         $"AND \"Email\" = @Email" +
                                                                         $"WHERE \"Id\" = @Id", newPatient);

            return patient;
        }
    }
}

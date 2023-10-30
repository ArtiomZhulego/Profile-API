using Dapper;
using Domain;
using Domain.Exceptions;
using Domain.Repositories;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace Persistance
{
    public class DoctorRepository : IDoctorRepository
    {
        private NpgsqlConnection connection;

        public DoctorRepository(IConfiguration configuration)
        {
            connection = new NpgsqlConnection(configuration.GetConnectionString("DefaultConnection"));
        }

        public async Task<Doctor> CreateAsync(Doctor _doctor, CancellationToken token)
        {
            if (connection.State == System.Data.ConnectionState.Closed)
                connection.Open();

            var doctor = await connection.QueryAsync<Doctor>($"INSERT INTO public.\"Doctor\" (\"Id\",\"Photo\", \"FirstName\", \"MiddleName\", \"LastName\", \"DateOfBirth\", \"Email\", \"SpecializationId\", \"OfficeId\", \"CareerStartYear\", \"DoctorStatuses\")" +
                                                             $"VALUES (@Id,@Photo,@FirstName,@MiddleName,@LastName,@DateOfBirth,@Email,@SpecializationId,@OfficeId,@CareerStartYear,@DoctorStatuses)",_doctor);

            connection.Close();

            return doctor.FirstOrDefault();
        }

        public async Task DeleteAsync(Guid doctorId, CancellationToken token)
        {
            if (connection.State == System.Data.ConnectionState.Closed)
                connection.Open();

            await connection.QueryAsync($"DELETE FROM public.\"Doctor\" Where \"Id\" = {doctorId}");

            connection.Close();
        }

        public async Task<List<Doctor>> FilterDoctorAsync(Guid officeId, Guid specialityId, CancellationToken token)
        {
            if (connection.State == System.Data.ConnectionState.Closed)
                connection.Open();

            var doctor = (List<Doctor>) await connection.QueryAsync<Doctor>($"SELECT * From public.\"Doctor\"" +
                                                                            $"WHERE \"SpecializationId\" = @SpecializationId",new { SpecializationId = specialityId});

            connection.Close();

            return doctor;
        }

        public async Task<List<Doctor>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            if (connection.State == System.Data.ConnectionState.Closed)
                connection.Open();

            var doctors = (List<Doctor>)await connection.QueryAsync<Doctor>("SELECT * FROM public.\"Doctor\"");

            connection.Close();

            return doctors;
        }

        public async Task<Doctor> GetByIdAsync(Guid doctorId, CancellationToken cancellationToken = default)
        {
            if (connection.State == System.Data.ConnectionState.Closed)
                connection.Open();

            var doctor = await connection.QueryAsync<Doctor>($"SELECT * " +
                                                             $"FROM public.\"Doctor\" WHERE \"Id\" = @Id",new { Id = doctorId });

            connection.Close();

            return doctor.FirstOrDefault();
        }

        public async Task<List<Doctor>> SearchByNameAsync(string fullName, CancellationToken token)
        {
            if (connection.State == System.Data.ConnectionState.Closed)
                connection.Open();

            var doctorsList = await connection.QueryAsync<Doctor>($"SELECT * From public.\"Doctor\" " +
                                                                  $"\"FirstName\" LIKE @FirstName, \"MiddleName\" LIKE @MiddleName, \"LastName\" LIKE @LastName", new { FirstName = fullName, MiddleName = fullName, LastName = fullName });

            connection.Close();

            return doctorsList.ToList();
        }

        public async Task<Doctor> UpdateAsync(Guid doctorId, Doctor _doctor, CancellationToken token)
        {
            if (connection.State == System.Data.ConnectionState.Closed)
                connection.Open();

            var doctor = (Doctor)await connection.QueryAsync<Doctor>($"UPDATE public.\"Doctor\" SET" +
                                                                    $"\"Photo\" = @Photo, " +
                                                                    $"\"FirstName\" = @FirstName, " +
                                                                    $"\"MiddleName\" = @MiddleName, " +
                                                                    $"\"LastName\" = @LastName, " +
                                                                    $"\"DateOfBirth\" = @DateOfBirth, " +
                                                                    $"\"Email\" = @Email, " +
                                                                    $"\"SpecializationId\" = @SpecializationId, " +
                                                                    $"\"OfficeId\" = @OfficeId, " +
                                                                    $"\"CareerStartYear\" = @CareerStartYear, " +
                                                                    $"\"DoctorStatuses\" = @DoctorStatuses" +
                                                                    $"WHERE \"Id\" = @Id",_doctor);

            connection.Close();

            return doctor;
        }

        public async Task<Doctor> UpdateStatusAsync(Guid doctorId, int statuseId, CancellationToken token)
        {
            if (connection.State == System.Data.ConnectionState.Closed)
                connection.Open();  

            var doctor = (Doctor)await connection.QueryAsync<Doctor>($"UPDATE FROM public.\"Doctor\" SET " +
                                                                     $"\"DoctorStatuses\" = @DoctorStatuses WHERE \"Id\" = @doctorId",
                                                                     new { DoctorStatuses = (DoctorStatuses)statuseId, doctorId });

            connection.Close();

            return doctor;
        }
    }
}

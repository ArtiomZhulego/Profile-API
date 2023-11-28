using Dapper;
using Domain;
using Domain.Enums;
using Domain.Repositories;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace Persistance
{
    public class DoctorRepository : IDoctorRepository
    {
        private readonly NpgsqlConnection _connection;

        public DoctorRepository(IConfiguration configuration)
        {
            _connection = new NpgsqlConnection(configuration.GetConnectionString("DefaultConnection"));
        }

        public async Task<Doctor> CreateAsync(Doctor doctor, CancellationToken token)
        {
            await _connection.QueryAsync<Doctor>($"INSERT INTO public.\"Doctor\" (\"Id\",\"Photo\", \"FirstName\", \"MiddleName\", \"LastName\", \"DateOfBirth\", \"Email\", \"SpecializationId\", \"OfficeId\", \"CareerStartYear\", \"DoctorStatuses\",\"AccountId\")" +
                                                             $"VALUES (@Id,@Photo,@FirstName,@MiddleName,@LastName,@DateOfBirth,@Email,@SpecializationId,@OfficeId,@CareerStartYear,@DoctorStatuses,@AccountId)",_doctor);

            return doctor;
        }

        public async Task DeleteAsync(Guid doctorId, CancellationToken token) =>       
            await _connection.QueryAsync($"DELETE FROM public.\"Doctor\" Where \"Id\" = {doctorId}");

        public async Task<IEnumerable<Doctor>> FilterDoctorAsync(Guid officeId, Guid specialityId, CancellationToken token) =>
            await _connection.QueryAsync<Doctor>($"SELECT * From public.\"Doctor\"" +
                                                                            $"WHERE \"SpecializationId\" = @SpecializationId", new { SpecializationId = specialityId });
        public async Task<IEnumerable<Doctor>> GetAllAsync(CancellationToken cancellationToken = default) =>
            await _connection.QueryAsync<Doctor>("SELECT * FROM public.\"Doctor\"");


        public async Task<Doctor> GetByIdAsync(Guid doctorId, CancellationToken cancellationToken = default)
        {
            var doctor = await _connection.QueryAsync<Doctor>($"SELECT * " +
                                                             $"FROM public.\"Doctor\" WHERE \"Id\" = @Id",new { Id = doctorId });

            return doctor.FirstOrDefault();
        }

        public async Task<IEnumerable<Doctor>> SearchByNameAsync(string fullName, CancellationToken token) =>
            await _connection.QueryAsync<Doctor>($"SELECT * From public.\"Doctor\" WHERE" +
                                                                  $"\"FirstName\" LIKE @FirstName OR \"MiddleName\" LIKE @MiddleName OR \"LastName\" LIKE @LastName", new { FirstName = fullName, MiddleName = fullName, LastName = fullName });

        

        public async Task<Doctor> UpdateAsync(Guid doctorId, Doctor doctor, CancellationToken token)
        {
            await _connection.QueryAsync<Doctor>($"UPDATE public.\"Doctor\" SET" +
                                                                    $"\"Photo\" = @Photo, " +
                                                                    $"\"FirstName\" = @FirstName, " +
                                                                    $"\"MiddleName\" = @MiddleName, " +
                                                                    $"\"LastName\" = @LastName, " +
                                                                    $"\"DateOfBirth\" = @DateOfBirth, " +
                                                                    $"\"Email\" = @Email, " +
                                                                    $"\"SpecializationId\" = @SpecializationId, " +
                                                                    $"\"OfficeId\" = @OfficeId, " +
                                                                    $"\"CareerStartYear\" = @CareerStartYear, " +
                                                                    $"\"DoctorStatuses\" = @DoctorStatuses " +
                                                                    $"WHERE \"Id\" = @Id",doctor);

            return doctor;
        }

        public async Task<Doctor> UpdateStatusAsync(Guid doctorId, DoctorStatuses statuse, CancellationToken token)
        {
            var doctor = await _connection.QueryAsync<Doctor>($"UPDATE public.\"Doctor\" SET " +
                                                                     $"\"DoctorStatuses\" = @DoctorStatuses WHERE \"Id\" = @DoctorId",
                                                                     new { DoctorStatuses = statuse, DoctorId = doctorId });

            return doctor.FirstOrDefault();
        }
    }
}

using Dapper;
using Domain;
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
            await connection.QueryAsync<Doctor>($"INSERT INTO public.\"Doctor\" (\"Id\",\"Photo\", \"FirstName\", \"MiddleName\", \"LastName\", \"DateOfBirth\", \"Email\", \"SpecializationId\", \"OfficeId\", \"CareerStartYear\", \"DoctorStatuses\")" +
                                                             $"VALUES (@Id,@Photo,@FirstName,@MiddleName,@LastName,@DateOfBirth,@Email,@SpecializationId,@OfficeId,@CareerStartYear,@DoctorStatuses)",_doctor);

            return _doctor;
        }

        public async Task DeleteAsync(Guid doctorId, CancellationToken token)
        {
            await connection.QueryAsync($"DELETE FROM public.\"Doctor\" Where \"Id\" = {doctorId}");
        }

        public async Task<List<Doctor>> FilterDoctorAsync(Guid officeId, Guid specialityId, CancellationToken token)
        {
            var doctor = await connection.QueryAsync<Doctor>($"SELECT * From public.\"Doctor\"" +
                                                                            $"WHERE \"SpecializationId\" = @SpecializationId",new { SpecializationId = specialityId});

            return doctor.ToList();
        }

        public async Task<List<Doctor>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var doctors = await connection.QueryAsync<Doctor>("SELECT * FROM public.\"Doctor\"");

            return doctors.ToList();
        }

        public async Task<Doctor> GetByIdAsync(Guid doctorId, CancellationToken cancellationToken = default)
        {
            var doctor = await connection.QueryAsync<Doctor>($"SELECT * " +
                                                             $"FROM public.\"Doctor\" WHERE \"Id\" = @Id",new { Id = doctorId });

            return doctor.FirstOrDefault();
        }

        public async Task<List<Doctor>> SearchByNameAsync(string fullName, CancellationToken token)
        {
            var doctorsList = await connection.QueryAsync<Doctor>($"SELECT * From public.\"Doctor\" " +
                                                                  $"\"FirstName\" LIKE @FirstName, \"MiddleName\" LIKE @MiddleName, \"LastName\" LIKE @LastName", new { FirstName = fullName, MiddleName = fullName, LastName = fullName });

            return doctorsList.ToList();
        }

        public async Task<Doctor> UpdateAsync(Guid doctorId, Doctor _doctor, CancellationToken token)
        {
            await connection.QueryAsync<Doctor>($"UPDATE public.\"Doctor\" SET" +
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

            return _doctor;
        }

        public async Task<Doctor> UpdateStatusAsync(Guid doctorId, int statuseId, CancellationToken token)
        {
            var doctor = (Doctor)await connection.QueryAsync<Doctor>($"UPDATE FROM public.\"Doctor\" SET " +
                                                                     $"\"DoctorStatuses\" = @DoctorStatuses WHERE \"Id\" = @doctorId",
                                                                     new { DoctorStatuses = (DoctorStatuses)statuseId, doctorId });

            return doctor;
        }
    }
}

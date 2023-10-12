using Dapper;
using Domain;
using Domain.Exceptions;
using Domain.Repositories;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace Persistance
{
    public class DoctorContext : IDoctorRepository
    {
        private NpgsqlConnection connection;

        public DoctorContext(IConfiguration configuration)
        {
            connection = new NpgsqlConnection(configuration.GetConnectionString("DefaultConnection"));
        }

        public async Task<Doctor> CreateAsync(Doctor _doctor, CancellationToken token)
        {
            connection.Open();

            var doctor = (Doctor)await connection.QueryAsync<Doctor>($"INSERT INTO public.\"Doctor\" (\"Photo\", \"FirstName\", \"MiddleName\", \"LastName\", \"DateOfBirth\", \"Email\", \"SpecializationId\", \"OfficeId\", \"CareerStartYear\", \"DoctorStatuses\")" +
                                                                    $"VALUES ({_doctor.Photo},{_doctor.FirstName},{_doctor.MiddleName},{_doctor.LastName}," +
                                                                    $"{_doctor.DateOfBirth},{_doctor.Email},{_doctor.SpecializationId},{_doctor.OfficeId},{_doctor.CareerStartYear},{_doctor.DoctorStatuses})");

            connection.Close();

            return doctor;
        }

        public async Task DeleteAsync(Guid doctorId, CancellationToken token)
        {
            connection.Open();

            await connection.QueryAsync($"DELETE FROM public.\"Doctor\" Where \"Id\" = {doctorId}");

            connection.Close();
        }

        public async Task<List<Doctor>> FilterDoctorAsync(Guid officeId, Guid specialityId, CancellationToken token)
        {
            connection.Open();

            var doctor = (List<Doctor>) await connection.QueryAsync<Doctor>($"SELECT * From public.\"Doctor\"" +
                                                                            $"WHERE \"SpecializationId\" = '{specialityId}'");

            connection.Close();

            return doctor;
        }

        public async Task<List<Doctor>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            connection.Open();

            var doctors = (List<Doctor>)await connection.QueryAsync<Doctor>("SELECT \"Id\", \"Photo\", \"FirstName\", \"MiddleName\", \"LastName\", \"DateOfBirth\", \"Email\", \"SpecializationId\", \"OfficeId\", \"CareerStartYear\", \"DoctorStatuses\" FROM public.\"Doctor\";");

            connection.Close();

            return doctors;
        }

        public async Task<Doctor> GetByIdAsync(Guid doctorId, CancellationToken cancellationToken = default)
        {
            connection.Open();

            var doctor = connection.QueryAsync<Doctor>($"SELECT \"Id\", \"Photo\", \"FirstName\", \"MiddleName\", \"LastName\", \"DateOfBirth\", \"Email\", \"SpecializationId\", \"OfficeId\", \"CareerStartYear\", \"DoctorStatuses\"\r\n" +
                                                       $"FROM public.\"Doctor\" WHERE \"Id\" = '{doctorId}';").Result.FirstOrDefault();

            connection.Close();

            if (doctor == null)
            {
                throw new DoctorNotFoundException(doctorId);
            }

            return doctor;
        }

        public async Task<List<Doctor>> SearchByNameAsync(string fullName, CancellationToken token)
        {
            connection.Open();

            var doctorsList = (List<Doctor>)await connection.QueryAsync<Doctor>($"SELECT * From Doctor " +
                                                                                $"\"FirstName\" LIKE '{fullName}', \"MiddleName\" LIKE '{fullName}', \"LastName\" LIKE '{fullName}'");

            connection.Close();

            return doctorsList;
        }

        public async Task<Doctor> UpdateAsync(Guid doctorId, Doctor _doctor, CancellationToken token)
        {
            connection.Open();

            var doctor = (Doctor)await connection.QueryAsync<Doctor>($"UPDATE public.\"Doctor\"" +
                                                                    $"SET \"Id\" = '{_doctor.Id}', \"Photo\" = '{_doctor.Photo}', \"FirstName\" = '{_doctor.FirstName}', \"MiddleName\" = '{_doctor.MiddleName}', \"LastName\" = '{_doctor.LastName}', \"DateOfBirth\" = '{_doctor.DateOfBirth}', \"Email\" = '{_doctor.Email}', \"SpecializationId\" = '{_doctor.SpecializationId}', \"OfficeId\" = '{_doctor.OfficeId}', \"CareerStartYear\" = '{_doctor.CareerStartYear}', \"DoctorStatuses\" = '{_doctor.DoctorStatuses}'" +
                                                                    $"WHERE \"Id\" = '{doctorId}';");

            connection.Close();

            return doctor;
        }

        public async Task<Doctor> UpdateStatusAsync(Guid doctorId, int statuseId, CancellationToken token)
        {
            connection.Open();

            var doctor = (Doctor)await connection.QueryAsync<Doctor>($"UPDATE FROM public.\"Doctor\" SET \"DoctorStatuses\" = '{(DoctorStatuses) statuseId}' WHERE \"Id\" = '{doctorId}'");

            connection.Close();

            return doctor;
        }
    }
}

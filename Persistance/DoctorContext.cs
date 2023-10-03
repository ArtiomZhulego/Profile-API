using Dapper;
using Domain;
using Domain.Exceptions;
using Domain.Repositories;
using Npgsql;

namespace Persistance
{
    public class DoctorContext : IDoctorRepository
    {
        private NpgsqlConnection connection = new NpgsqlConnection("Server=localhost;Port=5432;Database=postgres;User Id=postgres;Password=QwErTy135790;");

        public async Task<Doctor> CreateAsync(Doctor _doctor, CancellationToken token)
        {
            connection.Open();

            var doctor = (Doctor)await connection.QueryAsync<Doctor>($"INSERT INTO Doctor " +
                                                                    $"(Photo, FirstName, MiddleName, LastName, DateOfBirth, Email, SpecializationId, OfficeId, CareerStartYear, DoctorStatuses)" +
                                                                    $"VALUES ({_doctor.Photo},{_doctor.FirstName},{_doctor.MiddleName},{_doctor.LastName}," +
                                                                    $"{_doctor.DateOfBirth},{_doctor.Email},{_doctor.SpecializationId},{_doctor.OfficeId},{_doctor.CareerStartYear},{_doctor.DoctorStatuses})");

            connection.Close();

            return doctor;
        }

        public async Task DeleteAsync(Guid doctorId, CancellationToken token)
        {
            connection.Open();

            await connection.QueryAsync($"DELETE FROM Doctor Where Doctor.Id = {doctorId}");

            connection.Close();
        }

        public async Task<List<Doctor>> FilterDoctorAsync(Guid officeId, Guid specialityId, CancellationToken token)
        {
            connection.Open();

            var doctor = (List<Doctor>) await connection.QueryAsync<Doctor>($"SELECT * From Doctor" +
                                                                            $"WHERE Doctor.SpecializationId = {specialityId}");

            connection.Close();

            return doctor;
        }

        public async Task<List<Doctor>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            connection.Open();

            var doctorsList = (List<Doctor>)await connection.QueryAsync<Doctor>($"SELECT * From Doctor");

            connection.Close();

            return doctorsList;
        }

        public async Task<Doctor> GetByIdAsync(Guid doctorId, CancellationToken cancellationToken = default)
        {
            connection.Open();

            var doctor = (Doctor) await connection.QueryAsync<Doctor>($"SELECT * From Doctor WHERE Doctor.Id = {doctorId}");

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
                                                                                $"WHERE Doctor.FirstName LIKE {fullName} OR " +
                                                                                $"Doctor.MiddleName LIKE {fullName} OR" +
                                                                                $"Doctor.LastName LIKE {fullName}");

            connection.Close();

            return doctorsList;
        }

        public async Task<Doctor> UpdateAsync(Guid doctorId, Doctor _doctor, CancellationToken token)
        {
            connection.Open();

            var doctor = (Doctor)await connection.QueryAsync<Doctor>($"UPDATE Doctor SET Doctor.DoctorStatuse = {_doctor.DoctorStatuses} " +
                                                                     $"AND Doctor.Photo = {_doctor.Photo} " +
                                                                     $"AND Doctor.FirstName = {_doctor.FirstName}" +
                                                                     $"AND Doctor.MiddleName = {_doctor.MiddleName}" +
                                                                     $"AND Doctor.DateOfBirth = {_doctor.DateOfBirth}" +
                                                                     $"AND Doctor.Email = {_doctor.Email}" +
                                                                     $"AND Doctor.SpecializationId = {_doctor.SpecializationId}" +
                                                                     $"AND Doctor.OfficeId = {_doctor.OfficeId}" +
                                                                     $"AND Doctor.CareerStartYear = {_doctor.CareerStartYear}" +
                                                                     $"WhHERE Doctor.Id = {_doctor.Id}");

            connection.Close();

            return doctor;
        }

        public async Task<Doctor> UpdateStatusAsync(Guid doctorId, int statuseId, CancellationToken token)
        {
            connection.Open();

            var doctor = (Doctor)await connection.QueryAsync<Doctor>($"UPDATE FROM Doctor SET Doctor.DoctorStatuse = {(DoctorStatuses) statuseId}");

            connection.Close();

            return doctor;
        }
    }
}

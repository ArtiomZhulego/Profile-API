using Dapper;
using Domain;
using Domain.Exceptions;
using Domain.Repositories;
using Npgsql;

namespace Persistance
{
    public class PatientContext : IPatientRepository
    {
        private NpgsqlConnection connection = new NpgsqlConnection("Server=localhost;Port=5432;Database=postgres;User Id=postgres;Password=QwErTy135790;");

        public async Task<Patient> CreateAsync(Patient _patient, CancellationToken token)
        {
            connection.Open();

            var patient = (Patient) await connection.QueryAsync<Patient>($"INSERT INTO Patient (FirstName, MiddleName, LastName, Photo, PhoneNumber, DateOfBirth)" +
                                                                         $"VALUES ({_patient.FirstName},{_patient.MiddleName},{_patient.LastName},{_patient.Photo},{_patient.PhoneNumber},{_patient.DateOfBirth})");

            if (patient == null)
            {
                throw new BadRequestException($"Patient with {_patient.Id} identifier does not created");
            }

            connection.Close();

            return patient;
        }

        public async Task DeleteAsync(Guid patientId, CancellationToken token)
        {
            connection.Open();

            await connection.QueryAsync($"DELETE FROM Patient Where Patient.Id = {patientId}");

            connection.Close();
        }

        public async Task<List<Patient>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            connection.Open();

            var patientList = (List<Patient>) await connection.QueryAsync<Patient>($"SELECT * From Patient");

            connection.Close();

            return patientList;
        }

        public async Task<Patient> GetByIdAsync(Guid patientId, CancellationToken cancellationToken = default)
        {
            connection.Open();

            var patient = (Patient) await connection.QueryAsync<Patient>($"SELECT * FROM Patient WHERE Patient.Id = {patientId}");

            connection.Close();

            if (patient == null)
            {
                throw new PatientNotFoundException(patientId);
            }

            return patient;
        }

        public async Task<List<Patient>> SearchByNameAsync(string fullName, CancellationToken token)
        {
            connection.Open();

            var patientsList = (List<Patient>) await connection.QueryAsync<Patient>($"Select * FROM Patient " +
                                                                                    $"WHERE Patient.FirstName LIKE {fullName} OR " +
                                                                                    $"Patient.MiddleName LIKE {fullName} OR" +
                                                                                    $"Patient.LastName LIKE {fullName}");

            connection.Close();

            return patientsList;
        }

        public async Task<Patient> UpdateAsync(Guid patientId, Patient newPatient, CancellationToken token)
        {
            connection.Open();

            var patient = (Patient) await connection.QueryAsync<Patient>($"UPDATE Patient SET Patient.FirstName = {newPatient.FirstName}" +
                                                                         $"AND Patient.MiddleName = {newPatient.MiddleName}" +
                                                                         $"AND Patient.LastName = {newPatient.LastName}" +
                                                                         $"AND Patient.Photo = {newPatient.Photo}" +
                                                                         $"AND Patient.PhoneNumber = {newPatient.PhoneNumber} " +
                                                                         $"AND Patient.DateOfBirth = {newPatient.DateOfBirth}");

            connection.Close();

            return patient;
        }
    }
}

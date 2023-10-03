namespace Domain.Repositories
{
    public interface IPatientRepository
    {
        Task<List<Patient>> GetAllAsync(CancellationToken cancellationToken = default);

        Task<Patient> GetByIdAsync(Guid patientId, CancellationToken cancellationToken = default);

        Task<Patient> UpdateAsync(Guid patientId, Patient newPatient, CancellationToken token);

        Task DeleteAsync(Guid patientId, CancellationToken token);

        Task<Patient> CreateAsync(Patient patient, CancellationToken token);

        Task<List<Patient>> SearchByNameAsync(string fullName, CancellationToken token);
    }
}

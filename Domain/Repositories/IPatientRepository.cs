namespace Domain.Repositories
{
    public interface IPatientRepository
    {
        Task<List<Patient>> GetAllAsync(CancellationToken cancellationToken = default);

        Task<Patient> GetByIdAsync(Guid patientId, CancellationToken cancellationToken = default);

        Task<Patient> Update(Guid patientId, Patient newPatient, CancellationToken token);

        Task Delete(Guid patientId, CancellationToken token);

        Task<Patient> Create(CancellationToken token);

        Task<List<Patient>> SearchByName(string fullName, CancellationToken token);
    }
}

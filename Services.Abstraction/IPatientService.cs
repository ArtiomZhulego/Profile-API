using Contracts;

namespace Services.Abstraction
{
    public interface IPatientService
    {
        Task<IEnumerable<PatientDTO>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<PatientDTO> GetByIdAsync(Guid patientId, CancellationToken cancellationToken = default);
        Task<PatientDTO> UpdateAsync(Guid patientId, PatientDTO newPatient, CancellationToken token);
        Task DeleteAsync(Guid patientId, CancellationToken token);
        Task<PatientDTO> CreateAsync(PatientDTO patientDTO, CancellationToken token);
        Task<IEnumerable<PatientDTO>> SearchByNameAsync(string fullName, CancellationToken token);
    }
}

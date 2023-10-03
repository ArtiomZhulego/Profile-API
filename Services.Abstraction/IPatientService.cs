using Contracts;

namespace Services.Abstraction
{
    public interface IPatientService
    {
        Task<List<PatientDTO>> GetAllAsync(CancellationToken cancellationToken = default);

        Task<PatientDTO> GetByIdAsync(Guid patientId, CancellationToken cancellationToken = default);

        Task<PatientDTO> UpdateAsync(Guid patientId, PatientDTO newPatient, CancellationToken token);

        Task DeleteAsync(Guid patientId, CancellationToken token);

        Task<PatientDTO> CreateAsync(PatientDTO patientDTO, CancellationToken token);

        Task<List<PatientDTO>> SearchByNameAsync(string fullName, CancellationToken token);
    }
}

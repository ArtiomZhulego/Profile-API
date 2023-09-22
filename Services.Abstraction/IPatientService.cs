using Contracts;
using Domain;

namespace Services.Abstraction
{
    public interface IPatientService
    {
        Task<List<PatientDTO>> GetAllAsync(CancellationToken cancellationToken = default);

        Task<PatientDTO> GetByIdAsync(Guid patientId, CancellationToken cancellationToken = default);

        Task<PatientDTO> Update(Guid patientId, PatientDTO newPatient, CancellationToken token);

        Task Delete(Guid patientId, CancellationToken token);

        Task<PatientDTO> Create(CancellationToken token);

        Task<List<PatientDTO>> SearchByName(string fullName, CancellationToken token);
    }
}

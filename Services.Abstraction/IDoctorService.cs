using Contracts;
using Domain.Enums;

namespace Services.Abstraction
{
    public interface IDoctorService
    {
        Task<IEnumerable<DoctorDTO>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<DoctorDTO> GetByIdAsync(Guid doctorId, CancellationToken cancellationToken = default);
        Task<DoctorDTO> UpdateAsync(Guid doctorId, DoctorDTO doctor, CancellationToken token);
        Task<DoctorDTO> UpdateStatusAsync(Guid doctorId, DoctorStatuses statuseId, CancellationToken token);
        Task DeleteAsync(Guid doctorId, CancellationToken token);
        Task<DoctorDTO> CreateAsync(DoctorDTO doctorDTO, CancellationToken token);
        Task<IEnumerable<DoctorDTO>> FilterDoctorAsync(Guid officeId, Guid specialityId, CancellationToken token);
        Task<IEnumerable<DoctorDTO>> SearchByNameAsync(string fullName, CancellationToken token);
    }
}

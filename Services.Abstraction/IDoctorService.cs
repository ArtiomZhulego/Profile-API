using Contracts;

namespace Services.Abstraction
{
    public interface IDoctorService
    {
        Task<List<DoctorDTO>> GetAllAsync(CancellationToken cancellationToken = default);

        Task<DoctorDTO> GetByIdAsync(Guid doctorId, CancellationToken cancellationToken = default);

        Task<DoctorDTO> UpdateAsync(Guid doctorId, DoctorDTO doctor, CancellationToken token);

        Task<DoctorDTO> UpdateStatusAsync(Guid doctorId, int statuseId, CancellationToken token);

        Task DeleteAsync(Guid doctorId, CancellationToken token);

        Task<DoctorDTO> CreateAsync(DoctorDTO doctorDTO, CancellationToken token);

        Task<List<DoctorDTO>> FilterDoctorAsync(Guid officeId, Guid specialityId, CancellationToken token);

        Task<List<DoctorDTO>> SearchByNameAsync(string fullName, CancellationToken token);
    }
}

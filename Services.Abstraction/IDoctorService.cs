using Contracts;

namespace Services.Abstraction
{
    public interface IDoctorService
    {
        Task<List<DoctorDTO>> GetAllAsync(CancellationToken cancellationToken = default);

        Task<DoctorDTO> GetByIdAsync(Guid doctorId, CancellationToken cancellationToken = default);

        Task<DoctorDTO> Update(Guid doctorId, DoctorDTO doctor, CancellationToken token);

        Task<DoctorDTO> PatchStatus(Guid doctorId, int statuseId, CancellationToken token);

        Task Delete(Guid doctorId, CancellationToken token);

        Task<DoctorDTO> Create(CancellationToken token);

        Task<List<DoctorDTO>> FilterDoctor(Guid officeId, Guid specialityId, CancellationToken token);

        Task<List<DoctorDTO>> SearchByName(string fullName, CancellationToken token);
    }
}

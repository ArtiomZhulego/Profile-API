using Domain.Enums;

namespace Domain.Repositories
{
    public interface IDoctorRepository
    {
        Task<IEnumerable<Doctor>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<Doctor> GetByIdAsync(Guid doctorId, CancellationToken cancellationToken = default);
        Task<Doctor> UpdateAsync(Guid doctorId, Doctor doctor, CancellationToken token);
        Task<Doctor> UpdateStatusAsync(Guid doctorId, DoctorStatuses statuse, CancellationToken token);
        Task DeleteAsync(Guid doctorId, CancellationToken token);
        Task<Doctor> CreateAsync(Doctor doctor, CancellationToken token);
        Task<IEnumerable<Doctor>> FilterDoctorAsync(Guid officeId, Guid specialityId, CancellationToken token);
        Task<IEnumerable<Doctor>> SearchByNameAsync(string fullName, CancellationToken token);
    }
}

namespace Domain.Repositories
{
    public interface IDoctorRepository
    {
        Task<List<Doctor>> GetAllAsync(CancellationToken cancellationToken = default);

        Task<Doctor> GetByIdAsync(Guid doctorId, CancellationToken cancellationToken = default);

        Task<Doctor> UpdateAsync(Guid doctorId, Doctor doctor, CancellationToken token);

        Task<Doctor> UpdateStatusAsync(Guid doctorId, Guid statuseId, CancellationToken token);

        Task DeleteAsync(Guid doctorId, CancellationToken token);

        Task<Doctor> CreateAsync(CancellationToken token);

        Task<List<Doctor>> FilterDoctorAsync(Guid officeId, Guid specialityId, CancellationToken token);

        Task<List<Doctor>> SearchByNameAsync(string fullName, CancellationToken token);
    }
}

namespace Domain.Repositories
{
    public interface IDoctorRepository
    {
        Task<List<Doctor>> GetAllAsync(CancellationToken cancellationToken = default);

        Task<Doctor> GetByIdAsync(Guid doctorId, CancellationToken cancellationToken = default);

        Task<Doctor> Update(Guid doctorId, Doctor doctor, CancellationToken token);

        Task<Doctor> PatchStatus(Guid doctorId, int statuseId, CancellationToken token);

        Task Delete(Guid doctorId, CancellationToken token);

        Task<Doctor> Create(CancellationToken token);

        Task<List<Doctor>> FilterDoctor(Guid officeId, Guid specialityId, CancellationToken token);

        Task<List<Doctor>> SearchByName(string fullName, CancellationToken token);
    }
}

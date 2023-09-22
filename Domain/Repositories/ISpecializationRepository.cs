namespace Domain.Repositories
{
    public interface ISpecializationRepository
    {
        Task<List<Specializations>> GetAllAsync(CancellationToken cancellationToken = default);

        Task<Specializations> GetByIdAsync(Guid specializationId, CancellationToken cancellationToken = default);

        Task<Specializations> Update(Guid specializationId, Specializations specializations, CancellationToken token);

        Task Delete(Guid specializationId, CancellationToken token);

        Task<Specializations> Create(CancellationToken token);
    }
}

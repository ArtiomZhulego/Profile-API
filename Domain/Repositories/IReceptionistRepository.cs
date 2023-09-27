namespace Domain.Repositories
{
    public interface IReceptionistRepository
    {
        Task<List<Receptionist>> GetAllAsync(CancellationToken cancellationToken = default);

        Task<Receptionist> GetByIdAsync(Guid receptionistId, CancellationToken cancellationToken = default);

        Task<Receptionist> UpdateAsync(Guid receptionistId, Receptionist receptionist, CancellationToken token);

        Task DeleteAsync(Guid receptionistId, CancellationToken token);

        Task<Receptionist> CreateAsync(CancellationToken token);
    }
}

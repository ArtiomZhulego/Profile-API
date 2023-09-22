namespace Domain.Repositories
{
    public interface IReceptionistRepository
    {
        Task<List<Receptionist>> GetAllAsync(CancellationToken cancellationToken = default);

        Task<Receptionist> GetByIdAsync(Guid receptionistId, CancellationToken cancellationToken = default);

        Task<Receptionist> Update(Guid receptionistId, Receptionist receptionist, CancellationToken token);

        Task Delete(Guid receptionistId, CancellationToken token);

        Task<Receptionist> Create(CancellationToken token);
    }
}

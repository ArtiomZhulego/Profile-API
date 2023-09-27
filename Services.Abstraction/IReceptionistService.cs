using Contracts;

namespace Services.Abstraction
{
    public interface IReceptionistService
    {
        Task<List<ReceptionistDTO>> GetAllAsync(CancellationToken cancellationToken = default);

        Task<ReceptionistDTO> GetByIdAsync(Guid receptionistId, CancellationToken cancellationToken = default);

        Task<ReceptionistDTO> UpdateAsync(Guid receptionistId, ReceptionistDTO receptionist, CancellationToken token);

        Task DeleteAsync(Guid receptionistId, CancellationToken token);

        Task<ReceptionistDTO> CreateAsync(CancellationToken token);
    }
}

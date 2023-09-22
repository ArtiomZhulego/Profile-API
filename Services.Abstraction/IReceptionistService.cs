using Contracts;

namespace Services.Abstraction
{
    public interface IReceptionistService
    {
        Task<List<ReceptionistDTO>> GetAllAsync(CancellationToken cancellationToken = default);

        Task<ReceptionistDTO> GetByIdAsync(Guid receptionistId, CancellationToken cancellationToken = default);

        Task<ReceptionistDTO> Update(Guid receptionistId, ReceptionistDTO receptionist, CancellationToken token);

        Task Delete(Guid receptionistId, CancellationToken token);

        Task<ReceptionistDTO> Create(CancellationToken token);
    }
}

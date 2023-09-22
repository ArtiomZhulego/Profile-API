using Contracts;

namespace Services.Abstraction
{
    public interface ISpecializationService
    {
        Task<List<SpecializationDTO>> GetAllAsync(CancellationToken cancellationToken = default);

        Task<SpecializationDTO> GetByIdAsync(Guid specializationId, CancellationToken cancellationToken = default);

        Task<SpecializationDTO> Update(Guid specializationId, SpecializationDTO specializations, CancellationToken token);

        Task Delete(Guid specializationId, CancellationToken token);

        Task<SpecializationDTO> Create(CancellationToken token);
    }
}

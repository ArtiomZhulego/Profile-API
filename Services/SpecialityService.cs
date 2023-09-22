using Contracts;
using Services.Abstraction;
using Domain.Repositories;
using Contracts.CreatingDto;
using Domain.Exceptions;
using Contracts.UpdatingDto;

namespace Services
{
    public class SpecialityService : ISpecializationService
    {
        private readonly ISpecializationRepository _repository;

        public SpecialityService(ISpecializationRepository repository) => _repository = repository;

        public async Task<SpecializationDTO> Create(CancellationToken token)
        {
            var specialization = await _repository.Create(token);

            if (specialization is null)
            {
                throw new BadRequestException($"The receptionist could not be created");
            }

            return CreatingSpecializationDto.Adapt(specialization);
        }

        public async Task Delete(Guid specializationId, CancellationToken token)
        {
            await _repository.Delete(specializationId, token);
        }

        public async Task<List<SpecializationDTO>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return CreatingSpecializationDto.Adapt(await _repository.GetAllAsync(cancellationToken));
        }

        public async Task<SpecializationDTO> GetByIdAsync(Guid specializationId, CancellationToken cancellationToken = default)
        {
            return CreatingSpecializationDto.Adapt(await _repository.GetByIdAsync(specializationId, cancellationToken));
        }

        public async Task<SpecializationDTO> Update(Guid specializationId, SpecializationDTO specializations, CancellationToken token)
        {
            var _specialization = await _repository.GetByIdAsync(specializationId, token);

            if (_specialization is null)
            {
                throw new DoctorNotFoundException(specializationId);
            }

            await _repository.Update(specializationId, UpdatingSpecializationDto.Adapt(specializations), token);

            return CreatingSpecializationDto.Adapt(_specialization);
        }
    }
}

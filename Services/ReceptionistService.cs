using Contracts;
using Contracts.CreatingDto;
using Contracts.UpdatingDto;
using Domain.Exceptions;
using Domain.Repositories;
using Services.Abstraction;

namespace Services
{
    public class ReceptionistService : IReceptionistService
    {
        private readonly IReceptionistRepository _repository;

        public ReceptionistService(IReceptionistRepository repository) => _repository = repository;

        public async Task<ReceptionistDTO> Create(CancellationToken token)
        {
            var reseptionist = await _repository.Create(token);

            if (reseptionist is null)
            {
                throw new BadRequestException($"The receptionist could not be created");
            }

            return CreatingReceptionistDto.Adapt(reseptionist);
        }

        public async Task Delete(Guid receptionistId, CancellationToken token)
        {
            await _repository.Delete(receptionistId, token);
        }

        public async Task<List<ReceptionistDTO>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return CreatingReceptionistDto.Adapt(await _repository.GetAllAsync(cancellationToken));
        }

        public async Task<ReceptionistDTO> GetByIdAsync(Guid receptionistId, CancellationToken cancellationToken = default)
        {
            return CreatingReceptionistDto.Adapt(await _repository.GetByIdAsync(receptionistId, cancellationToken));
        }

        public async Task<ReceptionistDTO> Update(Guid receptionistId, ReceptionistDTO receptionist, CancellationToken token)
        {
            var _receptionist = await _repository.GetByIdAsync(receptionistId, token);

            if (_receptionist is null)
            {
                throw new DoctorNotFoundException(receptionistId);
            }

            await _repository.Update(receptionistId, UpdatingReceptionistDto.Adapt(receptionist), token);

            return CreatingReceptionistDto.Adapt(_receptionist);
        }
    }
}

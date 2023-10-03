using Contracts;
using Contracts.CreatingDto;
using Domain.Exceptions;
using Domain.Repositories;
using Services.Abstraction;

namespace Services
{
    public class ReceptionistService : IReceptionistService
    {
        private readonly IReceptionistRepository _repository;

        public ReceptionistService(IReceptionistRepository repository) => _repository = repository;

        public async Task<ReceptionistDTO> CreateAsync(ReceptionistDTO receptionistDTO, CancellationToken token)
        {
            var reseptionist = await _repository.CreateAsync(ReceptionistMapper.MapToReceptionist(receptionistDTO),token);

            if (reseptionist is null)
            {
                throw new BadRequestException($"The receptionist could not be created");
            }

            return ReceptionistMapper.MapToReceptionistDto(reseptionist);
        }

        public async Task DeleteAsync(Guid receptionistId, CancellationToken token)
        {
            await _repository.DeleteAsync(receptionistId, token);
        }

        public async Task<List<ReceptionistDTO>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return ReceptionistMapper.MapToReceptionistDto(await _repository.GetAllAsync(cancellationToken));
        }

        public async Task<ReceptionistDTO> GetByIdAsync(Guid receptionistId, CancellationToken cancellationToken = default)
        {
            return ReceptionistMapper.MapToReceptionistDto(await _repository.GetByIdAsync(receptionistId, cancellationToken));
        }

        public async Task<ReceptionistDTO> UpdateAsync(Guid receptionistId, ReceptionistDTO receptionist, CancellationToken token)
        {
            var _receptionist = await _repository.GetByIdAsync(receptionistId, token);

            if (_receptionist is null)
            {
                throw new DoctorNotFoundException(receptionistId);
            }

            await _repository.UpdateAsync(receptionistId, ReceptionistMapper.MapToReceptionist(receptionist), token);

            return ReceptionistMapper.MapToReceptionistDto(_receptionist);
        }
    }
}

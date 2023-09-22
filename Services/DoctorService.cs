using Contracts;
using Contracts.CreatingDto;
using Contracts.UpdatingDto;
using Domain;
using Domain.Exceptions;
using Domain.Repositories;
using Services.Abstraction;
using System.Threading;

namespace Services
{
    public class DoctorService : IDoctorService
    {
        private readonly IDoctorRepository _repository;

        public DoctorService(IDoctorRepository repository) => _repository = repository;
        
        public async Task<DoctorDTO> Create(CancellationToken token)
        {
            var doctor = await _repository.Create(token);

            if (doctor is null)
            {
                throw new BadRequestException($"The doctor could not be created");
            }

            return CreatingDoctorDto.Adapt(doctor);
        }

        public async Task Delete(Guid doctorId, CancellationToken token)
        {
            await _repository.Delete(doctorId, token);
        }

        public async Task<List<DoctorDTO>> FilterDoctor(Guid officeId, Guid specialityId, CancellationToken token)
        {
            return CreatingDoctorDto.Adapt(await _repository.FilterDoctor(officeId, specialityId, token));
        }

        public async Task<List<DoctorDTO>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return CreatingDoctorDto.Adapt(await _repository.GetAllAsync(cancellationToken));
        }

        public async Task<DoctorDTO> GetByIdAsync(Guid doctorId, CancellationToken cancellationToken = default)
        {
            return CreatingDoctorDto.Adapt(await _repository.GetByIdAsync(doctorId,cancellationToken));
        }

        public async Task<DoctorDTO> PatchStatus(Guid doctorId, int statuseId, CancellationToken token)
        {
            var doctor = await _repository.GetByIdAsync(doctorId, token);

            if (doctor is null)
            {
                throw new DoctorNotFoundException(doctorId);
            }

            await _repository.PatchStatus(doctorId, statuseId, token);

            return CreatingDoctorDto.Adapt(doctor);
        }

        public async Task<List<DoctorDTO>> SearchByName(string fullName, CancellationToken token)
        {
            return CreatingDoctorDto.Adapt(await _repository.SearchByName(fullName, token));
        }

        public async Task<DoctorDTO> Update(Guid doctorId, DoctorDTO doctor, CancellationToken token)
        {
            var _doctor = await _repository.GetByIdAsync(doctorId, token);

            if (_doctor is null)
            {
                throw new DoctorNotFoundException(doctorId);
            }

            await _repository.Update(doctorId, UpdatingDoctorDto.Adapt(doctor), token);

            return CreatingDoctorDto.Adapt(_doctor);
        }
    }
}

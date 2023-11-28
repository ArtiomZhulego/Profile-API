using Contracts;
using Contracts.CreatingDto;
using Domain.Enums;
using Domain.Exceptions;
using Domain.Repositories;
using Services.Abstraction;

namespace Services
{
    public class DoctorService : IDoctorService
    {
        private readonly IDoctorRepository _repository;

        public DoctorService(IDoctorRepository repository)
        {
            _repository = repository;
        }

        public async Task<DoctorDTO> CreateAsync(DoctorDTO doctorDTO,CancellationToken token)
        {
            var doctor = await _repository.CreateAsync(DoctorMapper.MapToDoctor(doctorDTO), token);

            if (doctor is null)
                throw new BadRequestException($"The doctor could not be created");

            return DoctorMapper.MapToDoctorDto(doctor);
        }

        public async Task DeleteAsync(Guid doctorId, CancellationToken token)
        {
            var doctor = await _repository.GetByIdAsync(doctorId,token);

            if (doctor is null)
                throw new EntityNotFoundException("Doctor not found");

            await _repository.DeleteAsync(doctorId, token);
        }

        public async Task<IEnumerable<DoctorDTO>> FilterDoctorAsync(Guid officeId, Guid specialityId, CancellationToken token)
        {
            return DoctorMapper.MapToDoctorDto(await _repository.FilterDoctorAsync(officeId, specialityId, token));
        }

        public async Task<IEnumerable<DoctorDTO>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return DoctorMapper.MapToDoctorDto(await _repository.GetAllAsync(cancellationToken));
        }

        public async Task<DoctorDTO> GetByIdAsync(Guid doctorId, CancellationToken cancellationToken = default)
        {
            var doctor = await _repository.GetByIdAsync(doctorId, cancellationToken);

            if (doctor is null)
                throw new EntityNotFoundException("Doctor not found");

            return DoctorMapper.MapToDoctorDto(await _repository.GetByIdAsync(doctorId,cancellationToken));
        }

        public async Task<DoctorDTO> UpdateStatusAsync(Guid doctorId, DoctorStatuses statuse, CancellationToken token)
        {
            var doctor = await _repository.GetByIdAsync(doctorId, token);

            if (doctor is null)
                throw new EntityNotFoundException("Doctor not found");

            await _repository.UpdateStatusAsync(doctorId, statuse, token);

            return DoctorMapper.MapToDoctorDto(doctor);
        }

        public async Task<IEnumerable<DoctorDTO>> SearchByNameAsync(string fullName, CancellationToken token)
        {
            return DoctorMapper.MapToDoctorDto(await _repository.SearchByNameAsync(fullName, token));
        }

        public async Task<DoctorDTO> UpdateAsync(Guid doctorId, DoctorDTO doctorDto, CancellationToken token)
        {
            var doctor = await _repository.GetByIdAsync(doctorId, token);

            if (doctor is null)
                throw new EntityNotFoundException("Doctor not found");

            await _repository.UpdateAsync(doctorId, DoctorMapper.MapToDoctor(doctorDto), token);

            return DoctorMapper.MapToDoctorDto(doctor);
        }
    }
}

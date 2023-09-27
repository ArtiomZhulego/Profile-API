﻿using Contracts;
using Contracts.CreatingDto;
using Domain.Exceptions;
using Domain.Repositories;
using Services.Abstraction;

namespace Services
{
    public class DoctorService : IDoctorService
    {
        private readonly IDoctorRepository _repository;

        public DoctorService(IDoctorRepository repository) => _repository = repository;
        
        public async Task<DoctorDTO> CreateAsync(CancellationToken token)
        {
            var doctor = await _repository.CreateAsync(token);

            if (doctor is null)
            {
                throw new BadRequestException($"The doctor could not be created");
            }

            return DoctorMapper.MapToDoctorDto(doctor);
        }

        public async Task DeleteAsync(Guid doctorId, CancellationToken token)
        {
            await _repository.DeleteAsync(doctorId, token);
        }

        public async Task<List<DoctorDTO>> FilterDoctorAsync(Guid officeId, Guid specialityId, CancellationToken token)
        {
            return DoctorMapper.MapToDoctorDto(await _repository.FilterDoctorAsync(officeId, specialityId, token));
        }

        public async Task<List<DoctorDTO>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return DoctorMapper.MapToDoctorDto(await _repository.GetAllAsync(cancellationToken));
        }

        public async Task<DoctorDTO> GetByIdAsync(Guid doctorId, CancellationToken cancellationToken = default)
        {
            return DoctorMapper.MapToDoctorDto(await _repository.GetByIdAsync(doctorId,cancellationToken));
        }

        public async Task<DoctorDTO> UpdateStatusAsync(Guid doctorId, Guid statuseId, CancellationToken token)
        {
            var doctor = await _repository.GetByIdAsync(doctorId, token);

            if (doctor is null)
            {
                throw new DoctorNotFoundException(doctorId);
            }

            await _repository.UpdateStatusAsync(doctorId, statuseId, token);

            return DoctorMapper.MapToDoctorDto(doctor);
        }

        public async Task<List<DoctorDTO>> SearchByNameAsync(string fullName, CancellationToken token)
        {
            return DoctorMapper.MapToDoctorDto(await _repository.SearchByNameAsync(fullName, token));
        }

        public async Task<DoctorDTO> UpdateAsync(Guid doctorId, DoctorDTO doctor, CancellationToken token)
        {
            var _doctor = await _repository.GetByIdAsync(doctorId, token);

            if (_doctor is null)
            {
                throw new DoctorNotFoundException(doctorId);
            }

            await _repository.UpdateAsync(doctorId, DoctorMapper.MapToDoctor(doctor), token);

            return DoctorMapper.MapToDoctorDto(_doctor);
        }
    }
}

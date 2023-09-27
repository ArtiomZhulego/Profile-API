using Contracts;
using Contracts.CreatingDto;
using Domain.Exceptions;
using Domain.Repositories;
using Services.Abstraction;

namespace Services
{
    public class PatientService : IPatientService
    {
        private readonly IPatientRepository _repository;

        public PatientService(IPatientRepository repository) => _repository = repository;

        public async Task<PatientDTO> CreateAsync(CancellationToken token)
        {
            var patient = await _repository.CreateAsync(token);

            if (patient is null)
            {
                throw new BadRequestException($"The patient could not be created");
            }

            return PatientMapper.MapToPatientDto(patient);
        }

        public async Task DeleteAsync(Guid patientId, CancellationToken token)
        {
            await _repository.DeleteAsync(patientId, token);
        }

        public async Task<List<PatientDTO>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return PatientMapper.MapToPatientDto(await _repository.GetAllAsync(cancellationToken));
        }

        public async Task<PatientDTO> GetByIdAsync(Guid patientId, CancellationToken cancellationToken = default)
        {
            return PatientMapper.MapToPatientDto(await _repository.GetByIdAsync(patientId, cancellationToken));
        }

        public async Task<List<PatientDTO>> SearchByNameAsync(string fullName, CancellationToken token)
        {
            return PatientMapper.MapToPatientDto(await _repository.SearchByNameAsync(fullName, token));
        }

        public async Task<PatientDTO> UpdateAsync(Guid patientId, PatientDTO newPatient, CancellationToken token)
        {
            var patient = await _repository.GetByIdAsync(patientId, token);

            if (patient is null)
            {
                throw new DoctorNotFoundException(patientId);
            }

            await _repository.UpdateAsync(patientId, PatientMapper.MapToPatient(newPatient), token);

            return PatientMapper.MapToPatientDto(patient);
        }
    }
}

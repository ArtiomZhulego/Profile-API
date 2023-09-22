using Contracts;
using Contracts.CreatingDto;
using Contracts.UpdatingDto;
using Domain;
using Domain.Exceptions;
using Domain.Repositories;
using Services.Abstraction;

namespace Services
{
    public class PatientService : IPatientService
    {
        private readonly IPatientRepository _repository;

        public PatientService(IPatientRepository repository) => _repository = repository;

        public async Task<PatientDTO> Create(CancellationToken token)
        {
            var patient = await _repository.Create(token);

            if (patient is null)
            {
                throw new BadRequestException($"The patient could not be created");
            }

            return CreatingPatientDto.Adapt(patient);
        }

        public async Task Delete(Guid patientId, CancellationToken token)
        {
            await _repository.Delete(patientId, token);
        }

        public async Task<List<PatientDTO>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return CreatingPatientDto.Adapt(await _repository.GetAllAsync(cancellationToken));
        }

        public async Task<PatientDTO> GetByIdAsync(Guid patientId, CancellationToken cancellationToken = default)
        {
            return CreatingPatientDto.Adapt(await _repository.GetByIdAsync(patientId, cancellationToken));
        }

        public async Task<List<PatientDTO>> SearchByName(string fullName, CancellationToken token)
        {
            return CreatingPatientDto.Adapt(await _repository.SearchByName(fullName, token));
        }

        public async Task<PatientDTO> Update(Guid patientId, PatientDTO newPatient, CancellationToken token)
        {
            var patient = await _repository.GetByIdAsync(patientId, token);

            if (patient is null)
            {
                throw new DoctorNotFoundException(patientId);
            }

            await _repository.Update(patientId, UpdatingPatientDto.Adapt(newPatient), token);

            return CreatingPatientDto.Adapt(patient);
        }
    }
}

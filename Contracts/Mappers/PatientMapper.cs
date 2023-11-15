using Domain;

namespace Contracts.CreatingDto
{
    public class PatientMapper
    {
        public static PatientDTO MapToPatientDto(Patient patient)
        {
            var patientDto = new PatientDTO()
            {
                Id = patient.Id,
                FirstName = patient.FirstName,
                LastName = patient.LastName,    
                DateOfBirth = patient.DateOfBirth,
                MiddleName = patient.MiddleName,
                PhoneNumber = patient.PhoneNumber,
                Photo = patient.Photo,
                AccountId = patient.AccountId,
                Email = patient.Email,
            };

            return patientDto;
        }

        public static IEnumerable<PatientDTO> MapToPatientDto(IEnumerable<Patient> patient)
        {
            var patients = patient.ToList().ConvertAll(patientDto => new PatientDTO()
            {
                Id = patientDto.Id,
                FirstName = patientDto.FirstName,
                LastName = patientDto.LastName,
                DateOfBirth = patientDto.DateOfBirth,
                MiddleName = patientDto.MiddleName,
                PhoneNumber = patientDto.PhoneNumber,
                Photo = patientDto.Photo,
                AccountId = patientDto.AccountId,
                Email = patientDto.Email,
            });

            return patients;
        }

        public static Patient MapToPatient(PatientDTO patientDTO)
        {
            var patient = new Patient()
            {
                Id = patientDTO.Id,
                FirstName = patientDTO.FirstName,
                LastName = patientDTO.LastName,
                DateOfBirth = patientDTO.DateOfBirth,
                MiddleName = patientDTO.MiddleName,
                PhoneNumber = patientDTO.PhoneNumber,
                Photo = patientDTO.Photo,
                AccountId = patientDTO.AccountId,
                Email = patientDTO.Email,
            };

            return patient;
        }
    }
}

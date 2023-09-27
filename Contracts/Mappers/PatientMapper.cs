using Domain;

namespace Contracts.CreatingDto
{
    public class PatientMapper
    {
        public static PatientDTO MapToPatientDto(Patient _patient)
        {
            var patient = new PatientDTO()
            {
                Id = _patient.Id,
                FirstName = _patient.FirstName,
                LastName = _patient.LastName,    
                DateOfBirth = _patient.DateOfBirth,
                MiddleName = _patient.MiddleName,
                PhoneNumber = _patient.PhoneNumber,
                Photo = _patient.Photo,
            };

            return patient;
        }

        public static List<PatientDTO> MapToPatientDto(List<Patient> patient)
        {
            var patients = patient.ConvertAll(_patient => new PatientDTO()
            {
                Id = _patient.Id,
                FirstName = _patient.FirstName,
                LastName = _patient.LastName,
                DateOfBirth = _patient.DateOfBirth,
                MiddleName = _patient.MiddleName,
                PhoneNumber = _patient.PhoneNumber,
                Photo = _patient.Photo,
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
            };

            return patient;
        }
    }
}

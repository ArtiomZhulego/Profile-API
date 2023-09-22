using Domain;

namespace Contracts.CreatingDto
{
    public class CreatingPatientDto
    {
        public static PatientDTO Adapt(Patient _patient)
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

        public static List<PatientDTO> Adapt(List<Patient> patient)
        {
            var allPatient = patient.ConvertAll(_patient => new PatientDTO()
            {
                Id = _patient.Id,
                FirstName = _patient.FirstName,
                LastName = _patient.LastName,
                DateOfBirth = _patient.DateOfBirth,
                MiddleName = _patient.MiddleName,
                PhoneNumber = _patient.PhoneNumber,
                Photo = _patient.Photo,
            });

            return allPatient;
        }
    }
}

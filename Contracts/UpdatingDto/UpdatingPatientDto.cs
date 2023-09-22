using Domain;

namespace Contracts.UpdatingDto
{
    public class UpdatingPatientDto
    {
        public static Patient Adapt(PatientDTO patientDTO)
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

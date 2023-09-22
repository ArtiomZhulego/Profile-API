using Domain;

namespace Contracts.UpdatingDto
{
    public class UpdatingDoctorDto
    {
        public static Doctor Adapt(DoctorDTO doctorDTO)
        {
            var doctor = new Doctor()
            {
                DateOfBirth = doctorDTO.DateOfBirth,
                SpecializationId = doctorDTO.SpecializationId,
                DoctorStatuses = doctorDTO.DoctorStatuses,
                CareerStartYear = doctorDTO.CareerStartYear,
                Email = doctorDTO.Email,
                FirstName = doctorDTO.FirstName,
                Id = doctorDTO.Id,
                LastName = doctorDTO.LastName,
                MiddleName = doctorDTO.MiddleName,
                OfficeId = doctorDTO.OfficeId,
                Photo = doctorDTO.Photo
            };

            return doctor;
        }
    }
}

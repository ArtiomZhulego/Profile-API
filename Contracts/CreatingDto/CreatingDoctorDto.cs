using Domain;

namespace Contracts.CreatingDto
{
    public class CreatingDoctorDto
    {
        public static DoctorDTO Adapt(Doctor _doctor)
        {
            var doctor = new DoctorDTO()
            {
                DateOfBirth = _doctor.DateOfBirth,
                SpecializationId = _doctor.SpecializationId,
                DoctorStatuses = _doctor.DoctorStatuses,
                CareerStartYear = _doctor.CareerStartYear,
                Email = _doctor.Email,
                FirstName = _doctor.FirstName,
                Id = _doctor.Id,
                LastName = _doctor.LastName,
                MiddleName = _doctor.MiddleName,
                OfficeId = _doctor.OfficeId,
                Photo = _doctor.Photo
            };

            return doctor;
        }

        public static List<DoctorDTO> Adapt(List<Doctor> _doctor)
        {
            var allDoctors = _doctor.ConvertAll(doctor => new DoctorDTO()
            {
                DateOfBirth = doctor.DateOfBirth,
                SpecializationId = doctor.SpecializationId,
                DoctorStatuses = doctor.DoctorStatuses,
                CareerStartYear = doctor.CareerStartYear,
                Email = doctor.Email,
                FirstName = doctor.FirstName,
                Id = doctor.Id,
                LastName = doctor.LastName,
                MiddleName = doctor.MiddleName,
                OfficeId = doctor.OfficeId,
                Photo = doctor.Photo
            });

            return allDoctors;
        }
    }
}

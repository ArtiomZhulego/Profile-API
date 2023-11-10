using Domain;

namespace Contracts.CreatingDto
{
    public class DoctorMapper
    {
        public static DoctorDTO MapToDoctorDto(Doctor _doctor)
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
                Photo = _doctor.Photo,
                AccountId = _doctor.AccountId,               
            };

            return doctor;
        }

        public static List<DoctorDTO> MapToDoctorDto(List<Doctor> _doctor)
        {
            var doctors = _doctor.ConvertAll(doctor => new DoctorDTO()
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
                Photo = doctor.Photo,
                AccountId = doctor.AccountId,
            });

            return doctors;
        }

        public static Doctor MapToDoctor(DoctorDTO doctorDTO)
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
                Photo = doctorDTO.Photo,
                AccountId = doctorDTO.AccountId,
            };

            return doctor;
        }
    }
}

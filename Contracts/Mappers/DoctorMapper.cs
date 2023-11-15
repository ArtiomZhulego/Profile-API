using Domain;

namespace Contracts.CreatingDto
{
    public class DoctorMapper
    {
        public static DoctorDTO MapToDoctorDto(Doctor doctor)
        {
            var doctorDto = new DoctorDTO()
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
            };

            return doctorDto;
        }

        public static IEnumerable<DoctorDTO> MapToDoctorDto(IEnumerable<Doctor> doctorList)
        {
            var doctors = doctorList.ToList().ConvertAll(doctorDto => new DoctorDTO()
            {
                DateOfBirth = doctorDto.DateOfBirth,
                SpecializationId = doctorDto.SpecializationId,
                DoctorStatuses = doctorDto.DoctorStatuses,
                CareerStartYear = doctorDto.CareerStartYear,
                Email = doctorDto.Email,
                FirstName = doctorDto.FirstName,
                Id = doctorDto.Id,
                LastName = doctorDto.LastName,
                MiddleName = doctorDto.MiddleName,
                OfficeId = doctorDto.OfficeId,
                Photo = doctorDto.Photo,
                AccountId = doctorDto.AccountId,
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

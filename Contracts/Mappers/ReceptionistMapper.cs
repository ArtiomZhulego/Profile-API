using Domain;

namespace Contracts.CreatingDto
{
    public class ReceptionistMapper
    {
        public static ReceptionistDTO MapToReceptionistDto(Receptionist _receptionist)
        {
            var receptionist = new ReceptionistDTO()
            {
                Email = _receptionist.Email,
                FirstName = _receptionist.FirstName,
                LastName = _receptionist.LastName,   
                MiddleName = _receptionist.MiddleName,
                OfficeId = _receptionist.OfficeId,
                Photo = _receptionist.Photo,
            };

            return receptionist;
        }

        public static List<ReceptionistDTO> MapToReceptionistDto(List<Receptionist> _receptionists)
        {
            var receptionists = _receptionists.ConvertAll(_receptionist => new ReceptionistDTO()
            {
                Email = _receptionist.Email,
                FirstName = _receptionist.FirstName,
                LastName = _receptionist.LastName,
                MiddleName = _receptionist.MiddleName,
                OfficeId = _receptionist.OfficeId,
                Photo = _receptionist.Photo,
            });

            return receptionists;
        }

        public static Receptionist MapToReceptionist(ReceptionistDTO _receptionist)
        {
            var receptionist = new Receptionist()
            {
                Email = _receptionist.Email,
                FirstName = _receptionist.FirstName,
                LastName = _receptionist.LastName,
                MiddleName = _receptionist.MiddleName,
                OfficeId = _receptionist.OfficeId,
                Photo = _receptionist.Photo,
            };

            return receptionist;
        }
    }
}

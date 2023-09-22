using Domain;

namespace Contracts.CreatingDto
{
    public class CreatingReceptionistDto
    {
        public static ReceptionistDTO Adapt(Receptionist _receptionist)
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

        public static List<ReceptionistDTO> Adapt(List<Receptionist> receptionists)
        {
            var allReceptionists = receptionists.ConvertAll(_receptionist => new ReceptionistDTO()
            {
                Email = _receptionist.Email,
                FirstName = _receptionist.FirstName,
                LastName = _receptionist.LastName,
                MiddleName = _receptionist.MiddleName,
                OfficeId = _receptionist.OfficeId,
                Photo = _receptionist.Photo,
            });

            return allReceptionists;
        }
    }
}

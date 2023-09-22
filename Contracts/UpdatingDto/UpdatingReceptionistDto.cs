using Domain;

namespace Contracts.UpdatingDto
{
    public class UpdatingReceptionistDto
    {
        public static Receptionist Adapt(ReceptionistDTO _receptionist)
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

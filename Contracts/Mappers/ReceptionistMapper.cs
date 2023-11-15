using Domain;

namespace Contracts.CreatingDto
{
    public class ReceptionistMapper
    {
        public static ReceptionistDTO MapToReceptionistDto(Receptionist receptionist)
        {
            var receptionistDto = new ReceptionistDTO()
            {
                Id = receptionist.Id,
                Email = receptionist.Email,
                FirstName = receptionist.FirstName,
                LastName = receptionist.LastName,   
                MiddleName = receptionist.MiddleName,
                OfficeId = receptionist.OfficeId,
                Photo = receptionist.Photo,
                AccountId = receptionist.AccountId,
            };

            return receptionistDto;
        }

        public static IEnumerable<ReceptionistDTO> MapToReceptionistDto(IEnumerable<Receptionist> receptionistList)
        {
            var receptionists = receptionistList.ToList().ConvertAll(receptionist => new ReceptionistDTO()
            {
                Id = receptionist.Id,
                Email = receptionist.Email,
                FirstName = receptionist.FirstName,
                LastName = receptionist.LastName,
                MiddleName = receptionist.MiddleName,
                OfficeId = receptionist.OfficeId,
                Photo = receptionist.Photo,
                AccountId = receptionist.AccountId,
            });

            return receptionists;
        }

        public static Receptionist MapToReceptionist(ReceptionistDTO receptionistDto)
        {
            var receptionist = new Receptionist()
            {
                Id = receptionistDto.Id,
                Email = receptionistDto.Email,
                FirstName = receptionistDto.FirstName,
                LastName = receptionistDto.LastName,
                MiddleName = receptionistDto.MiddleName,
                OfficeId = receptionistDto.OfficeId,
                Photo = receptionistDto.Photo,
                AccountId = receptionistDto.AccountId,
            };

            return receptionist;
        }
    }
}

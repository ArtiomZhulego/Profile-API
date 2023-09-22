using Domain;

namespace Contracts.CreatingDto
{
    public class CreatingSpecializationDto
    {
        public static SpecializationDTO Adapt(Specializations _specialization)
        {
            var specialization = new SpecializationDTO()
            {
                SpecializationName = _specialization.SpecializationName,
                Id = _specialization.Id,
                IsActive = _specialization.IsActive,
            };

            return specialization;
        }

        public static List<SpecializationDTO> Adapt(List<Specializations> _specializations)
        {
            var allSpecialization = _specializations.ConvertAll(specialization => new SpecializationDTO()
            {
                SpecializationName = specialization.SpecializationName,
                Id = specialization.Id,
                IsActive = specialization.IsActive,
            });

            return allSpecialization;
        }
    }
}

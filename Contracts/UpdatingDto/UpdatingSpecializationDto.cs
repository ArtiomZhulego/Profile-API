using Domain;

namespace Contracts.UpdatingDto
{
    public class UpdatingSpecializationDto
    {
        public static Specializations Adapt(SpecializationDTO _specialization)
        {
            var specialization = new Specializations()
            {
                SpecializationName = _specialization.SpecializationName,
                Id = _specialization.Id,
                IsActive = _specialization.IsActive,
            };

            return specialization;
        }
    }
}

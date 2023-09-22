namespace Domain.Exceptions
{
    public class SpecializationNotFound : Exception
    {
        public SpecializationNotFound(Guid specializationId) : base($"Doctor with {specializationId} not found") { }
    }
}

namespace Contracts
{
    public class SpecializationDTO
    {
        public Guid Id { get; set; }

        public string SpecializationName { get; set; } = String.Empty;

        public bool IsActive { get; set; }
    }
}

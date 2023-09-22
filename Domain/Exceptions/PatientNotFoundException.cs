namespace Domain.Exceptions
{
    public class PatientNotFoundException : Exception
    {
        public PatientNotFoundException(Guid patientId) : base($"Patient with {patientId} not found") { }
    }
}

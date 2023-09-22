namespace Domain.Exceptions
{
    public class ReceptionistNotFoundException : Exception
    {
        public ReceptionistNotFoundException(Guid receptionistId) : base($"Receptionist with {receptionistId} not found")  { }
    }
}

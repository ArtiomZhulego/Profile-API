namespace Domain.Exceptions
{
    public class DoctorNotFoundException : Exception
    {
        public DoctorNotFoundException(Guid doctorId) : base($"Doctor with {doctorId} not found") { } 
    }
}

using System.ComponentModel.DataAnnotations.Schema;
using Domain.Enums;

namespace Domain
{
    public class Doctor
    {
        public Guid Id { get; set; }

        public string Photo { get; set; } = string.Empty;

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string? MiddleName { get; set; } 

        public DateTime DateOfBirth { get; set; }

        public string Email { get; set; } = string.Empty;

        public Guid SpecializationId { get; set; }

        public Guid OfficeId { get; set; }

        public DateTime CareerStartYear { get; set; }

        public DoctorStatuses DoctorStatuses { get; set; }

        public Guid AccountId { get; set; }
    }
}

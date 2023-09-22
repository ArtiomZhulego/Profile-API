﻿namespace Domain
{
    public class Patient
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; } = string.Empty;

        public string? MiddleName { get; set; }

        public string LastName { get; set; } = string.Empty;

        public string? Photo { get; set; } 

        public int PhoneNumber { get; set; }

        public DateOnly DateOfBirth { get; set; }
    }
}

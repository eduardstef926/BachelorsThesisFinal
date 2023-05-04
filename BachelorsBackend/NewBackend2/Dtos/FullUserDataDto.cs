﻿namespace NewBackend2.Dtos
{
    public class FullUserDataDto
    {
        public int UserId { get; set; } 
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string? Email { get; set; }
        public bool isEmailConfirmed { get; set; }
    }
}
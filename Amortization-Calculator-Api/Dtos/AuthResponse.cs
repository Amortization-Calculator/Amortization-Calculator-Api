﻿namespace Amortization_Calculator_Api.Dtos
{
    public class AuthResponse
    {
        public string Message { get; set; }

        public bool isAuthSuccessful { get; set; }

        public string email { get; set; }

        public string userName { get; set; }

        public Gender gender { get; set; }

        public UserType userType { get; set; }

        public bool isActivated { get; set; }

        public int usageLease { get; set; }

        public string token { get; set; }

        public DateTime expireDate { get; set; }
    
    }
}

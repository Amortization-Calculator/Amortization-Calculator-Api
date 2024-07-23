﻿namespace Amortization_Calculator_Api.Config
{
    public class JwtOptions
    {
        public string Key { get; set; }

        public string Issuer { get; set; }

        public string Audience { get; set; }

        public double DurationInDays { get; set; }
    
    }
}

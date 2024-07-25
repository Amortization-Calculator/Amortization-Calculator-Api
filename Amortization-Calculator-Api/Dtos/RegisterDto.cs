namespace Amortization_Calculator_Api.Dtos
{
    public class RegisterDto
    {
        public required string firstName { get; set; }

        public required string lastName { get; set; }

        public required string userName { get; set; }

        public required string email { get; set; }

        public required string password { get; set; }

        public required string phoneNumber { get; set; }

        public required Gender gender { get; set; }

        public required UserType userType { get; set; }
    
    }
}

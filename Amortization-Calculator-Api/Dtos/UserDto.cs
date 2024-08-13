namespace Amortization_Calculator_Api.Dtos
{
    public class UserDto
    {
        public string UserName { get; set; }
        
        public string Email { get; set; }

        public required Gender gender { get; set; }

        public required UserType userType { get; set; }

        public required string phoneNumber { get; set; }

        public bool isActivated { get; set; }

        public int usageLease { get; set; }
    }
}

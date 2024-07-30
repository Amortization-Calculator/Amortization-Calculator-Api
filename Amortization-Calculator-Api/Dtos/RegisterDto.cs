using System.ComponentModel.DataAnnotations;

namespace Amortization_Calculator_Api.Dtos
{
    public class RegisterDto
    {

        public required string userName { get; set; }

        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        public required string email { get; set; }

        [DataType(DataType.Password)]
        public required string password { get; set; }

        [MaxLength(11)]
        public required string phoneNumber { get; set; }

        public required Gender gender { get; set; }

        public required UserType userType { get; set; }
    
    }
}

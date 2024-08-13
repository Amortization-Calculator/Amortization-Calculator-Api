using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Amortization_Calculator_Api.Models
{
    public class ApplicationUser: IdentityUser
    {

        [MaxLength(10)]
        public required Gender gender { get; set; }

        [MaxLength(10)]
        public required UserType userType { get; set; }

        public required bool isActivated { get; set; }

        public required int usageLease { get; set; }
    }
}

using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Amortization_Calculator_Api.Models
{
    public class ApplicationUser: IdentityUser
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MaxLength(100)]
        public required string firstName { get; set; }

        [MaxLength(100)]
        public required string lastName { get; set; }

        [MaxLength(10)]
        public required Gender gender { get; set; }

        [MaxLength(10)]
        public required UserType userType { get; set; }
    }
}

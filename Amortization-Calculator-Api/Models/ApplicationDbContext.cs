using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Amortization_Calculator_Api.Models
{
    public class ApplicationDbContext: IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationUser>()
                .Property(u => u.gender)
                .HasConversion(
                    g => g.ToString(),
                    g => (Gender)Enum.Parse(typeof(Gender), g)
                );

            modelBuilder.Entity<ApplicationUser>()
                .Property(u => u.userType)
                .HasConversion(
                    ut => ut.ToString(),
                    ut => (UserType)Enum.Parse(typeof(UserType), ut)
                );
        }
    }
}

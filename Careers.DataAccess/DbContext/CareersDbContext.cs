using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace Careers.DataAccess.Models
{
    public class CareersDbContext : IdentityDbContext<User, IdentityRole<int>, int>
    {
        public CareersDbContext(DbContextOptions<CareersDbContext> options)
            : base(options)
        {

        }
        public CareersDbContext()
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {

            base.OnModelCreating(builder);
            builder.Entity<User>().ToTable("Users");
            builder.Entity<IdentityRole<int>>().ToTable("Roles");
            builder.Entity<IdentityUserRole<int>>().ToTable("UserRoles");
            builder.Entity<IdentityUserClaim<int>>().ToTable("UserClaims");
            builder.Entity<IdentityUserLogin<int>>().ToTable("UserLogins");
            builder.Entity<IdentityRoleClaim<int>>().ToTable("RoleClaims");
            builder.Entity<IdentityUserToken<int>>().ToTable("UsersTokens");
        }

        public virtual DbSet<JobPosts> Jobs { get; set; }
        public virtual DbSet<Industries> Industries { get; set; }
        public virtual DbSet<AppliedJobs> AppliedJobs { get; set; }


    }
}

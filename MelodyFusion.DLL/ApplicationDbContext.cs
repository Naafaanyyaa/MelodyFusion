using MelodyFusion.DLL.Entities;
using MelodyFusion.DLL.Entities.Identity;
using MelodyFusion.DLL.EntityConfigurations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MelodyFusion.DLL
{
    public class ApplicationDbContext : IdentityDbContext<UserDto, RoleDto, string, IdentityUserClaim<string>, UserRole,
        IdentityUserLogin<string>, IdentityRoleClaim<string>, IdentityUserToken<string>>
    {
        public DbSet<SubscriptionDto> Subscription { get; set; }
        public DbSet<PhotoDto> Photo{ get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            Database.Migrate();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new RoleEntityConfiguration());
            builder.ApplyConfiguration(new UserEntityConfiguration());
            builder.ApplyConfiguration(new SubscriptionEntityConfiguration());
            builder.ApplyConfiguration(new PhotoEntityConfiguration());
        }
    }
}
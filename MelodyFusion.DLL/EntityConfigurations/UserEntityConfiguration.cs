using MelodyFusion.DLL.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MelodyFusion.DLL.EntityConfigurations
{
    public class UserEntityConfiguration : IEntityTypeConfiguration<UserDto>
    {
        public void Configure(EntityTypeBuilder<UserDto> builder)
        {
           builder.HasMany(ur => ur.UserRoles)
               .WithOne(u => u.UserDto)
               .HasForeignKey(x => x.UserId)
               .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

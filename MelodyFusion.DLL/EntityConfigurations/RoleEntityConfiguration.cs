using MelodyFusion.DLL.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MelodyFusion.DLL.EntityConfigurations
{
    public class RoleEntityConfiguration : IEntityTypeConfiguration<RoleDto>
    {
        public void Configure(EntityTypeBuilder<RoleDto> builder)
        {
            builder.HasMany(r => r.UserRoles)
                .WithOne(ru => ru.RoleDto)
                .HasForeignKey(x => x.RoleId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

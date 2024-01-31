using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetHospital.Data.Entities.Identity;

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

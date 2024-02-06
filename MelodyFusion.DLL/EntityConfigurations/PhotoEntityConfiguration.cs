using MelodyFusion.DLL.Entities;
using MelodyFusion.DLL.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MelodyFusion.DLL.EntityConfigurations
{
    public class PhotoEntityConfiguration : IEntityTypeConfiguration<PhotoDto>
    {
        public void Configure(EntityTypeBuilder<PhotoDto> builder)
        {
            builder.HasOne(x => x.User)
                .WithOne(p => p.Photo)
                .HasForeignKey<PhotoDto>(u => u.UserId);
        }
    }
}

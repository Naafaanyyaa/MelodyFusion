using MelodyFusion.DLL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MelodyFusion.DLL.EntityConfigurations
{
    public class AuthenticationStatisticEntityConfiguration : IEntityTypeConfiguration<AuthenticationStatisticDto>
    {
        public void Configure(EntityTypeBuilder<AuthenticationStatisticDto> builder)
        {
            builder.HasOne(x => x.User)
                .WithMany(x => x.AuthenticationStatistic)
                .HasForeignKey(u => u.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

using MelodyFusion.DLL.Entities;
using MelodyFusion.DLL.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MelodyFusion.DLL.EntityConfigurations
{
    public class SubscriptionEntityConfiguration : IEntityTypeConfiguration<SubscriptionDto>
    {
        public void Configure(EntityTypeBuilder<SubscriptionDto> builder)
        {
            builder.HasOne<UserDto>(u => u.User)
                .WithMany(x => x.Subscription)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MilkTeaBusinessObject.BusinessObject;

namespace MilkTeaBusinessObject.FluentAPI
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User");
            builder.HasKey(x => x.UserID);
            builder.Property(x => x.UserName);
            builder.Property(x => x.Password);
            builder.Property(x => x.FullName);
            builder.Property(x => x.Phone);
            builder.Property(x => x.UserAddress);
            builder.Property(x => x.Email);
            builder.Property(x => x.Status);
            builder.HasMany(x => x.Comments).WithOne(x => x.User).OnDelete(DeleteBehavior.NoAction);
            builder.HasMany(x => x.Orders).WithOne(x => x.User).OnDelete(DeleteBehavior.NoAction);

        }
    }
}

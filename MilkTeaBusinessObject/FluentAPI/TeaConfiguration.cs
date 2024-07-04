using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MilkTeaBusinessObject.BusinessObject;

namespace MilkTeaBusinessObject.FluentAPI
{
    public class TeaConfiguration : IEntityTypeConfiguration<Tea>
    {
        public void Configure(EntityTypeBuilder<Tea> builder)
        {
            builder.ToTable("Tea");
            builder.HasKey(x => x.TeaID);
            builder.Property(x => x.TeaName);
            builder.Property(x => x.Estimation);
            builder.Property(x => x.Price);
            builder.Property(x => x.TeaDescription);
            builder.Property(x => x.Status);
            builder.Property(x => x.Image);
            builder.HasMany(x => x.Comments).WithOne(x => x.Tea).OnDelete(DeleteBehavior.NoAction);
            builder.HasMany(x => x.OrderDetails).WithOne(x => x.Tea).OnDelete(DeleteBehavior.NoAction);
            builder.HasMany(x => x.DetailsMaterials).WithOne(x => x.Tea).OnDelete(DeleteBehavior.NoAction);


        }
    }
}

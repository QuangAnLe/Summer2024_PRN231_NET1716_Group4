using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MilkTeaBusinessObject.BusinessObject;

namespace MilkTeaBusinessObject.FluentAPI
{
    public class MaterialConfiguration : IEntityTypeConfiguration<Material>
    {
        public void Configure(EntityTypeBuilder<Material> builder)
        {
            builder.ToTable("Material");
            builder.HasKey(x => x.MaterialID);
            builder.Property(x => x.MaterialName);
            builder.Property(x => x.Quantity);
            builder.Property(x => x.Price);
            builder.Property(x => x.Status);
            builder.HasMany(x => x.DetailsMaterials).WithOne(x => x.Material).OnDelete(DeleteBehavior.NoAction);
        }
    }
}

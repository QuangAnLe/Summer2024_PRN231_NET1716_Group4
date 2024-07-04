using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MilkTeaBusinessObject.BusinessObject;

namespace MilkTeaBusinessObject.FluentAPI
{
    public class DistrictConfiguration : IEntityTypeConfiguration<District>
    {
        public void Configure(EntityTypeBuilder<District> builder)
        {
            builder.ToTable("District");
            builder.HasKey(x => x.DistrictID);
            builder.Property(x => x.DistrictName);
            builder.Property(x => x.WardName);
            builder.HasMany(x => x.Users).WithOne(x => x.District).OnDelete(DeleteBehavior.NoAction);

        }
    }
}

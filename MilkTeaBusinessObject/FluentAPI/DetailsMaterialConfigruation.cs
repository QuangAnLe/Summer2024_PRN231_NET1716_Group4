using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MilkTeaBusinessObject.BusinessObject;

namespace MilkTeaBusinessObject.FluentAPI
{
    public class DetailsMaterialConfigruation : IEntityTypeConfiguration<DetailsMaterial>
    {
        public void Configure(EntityTypeBuilder<DetailsMaterial> builder)
        {
            builder.ToTable("DetailsMaterial");
            builder.HasKey(x => x.DetailsMaterialID);
            builder.Property(x => x.Quanity);
            builder.Property(x => x.DetailMaterialName);

        }
    }
}

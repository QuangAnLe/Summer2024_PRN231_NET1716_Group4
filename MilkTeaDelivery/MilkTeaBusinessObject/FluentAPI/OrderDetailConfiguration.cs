using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MilkTeaBusinessObject.BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilkTeaBusinessObject.FluentAPI
{
    public class OrderDetailConfiguration : IEntityTypeConfiguration<OrderDetail>
    {
        public void Configure(EntityTypeBuilder<OrderDetail> builder)
        {
            builder.ToTable("OrderDetail");
            builder.HasKey(x => x.OrderID);
            builder.HasKey(x => x.TeaID);

            builder.Property(x => x.TotalPrice);
            builder.Property(x => x.Quantity);
            builder.Property(x => x.CostsIncurred);
           


        }
    }
}

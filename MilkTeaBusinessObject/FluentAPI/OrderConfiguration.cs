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
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {

            builder.ToTable("Order");
            builder.HasKey(x => x.OrderID);
            builder.Property(x => x.ReasonContent);
            builder.Property(x => x.TypeOrder);
            builder.Property(x => x.Status);
            builder.Property(x => x.StartDate);
            builder.Property(x => x.EndDate);
            builder.Property(x => x.ShipAddress);
            builder.HasMany(x => x.OrderDetails).WithOne(x => x.Order).OnDelete(DeleteBehavior.NoAction);

        }
    }
}

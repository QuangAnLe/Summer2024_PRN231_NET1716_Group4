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
    public class TaskUserCongfiguration : IEntityTypeConfiguration<TaskUser>
    {

        public void Configure(EntityTypeBuilder<TaskUser> builder)
        {
            builder.ToTable("Comments");
            builder.HasKey(x => x.TaskId);
            builder.Property(x => x.WorkName);
            builder.Property(x => x.WorkDescription);
            builder.Property(x => x.Status);
        }
    }
}

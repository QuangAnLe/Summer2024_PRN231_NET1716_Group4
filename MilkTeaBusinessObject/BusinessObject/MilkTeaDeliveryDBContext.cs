using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace MilkTeaBusinessObject.BusinessObject
{
    public class MilkTeaDeliveryDBContext : DbContext
    {
        public MilkTeaDeliveryDBContext()
        {
            
        }

        public MilkTeaDeliveryDBContext(DbContextOptions<MilkTeaDeliveryDBContext> opt) : base(opt) { }

        public virtual DbSet<Role>? Roles { get; set; }
        public virtual DbSet<Comment>? Comments { get; set; }
        public virtual DbSet<District>? Districts { get; set; }
        public virtual DbSet<Material>? Materials { get; set; }
        public virtual DbSet<DetailsMaterial>? DetailsMaterials { get; set; }
        public virtual DbSet<Order>? Orders { get; set; }
        public virtual DbSet<OrderDetail>? OrderDetails { get; set; }
        public virtual DbSet<Tea>? Teas { get; set; }

        public virtual DbSet<User>? Users { get; set; }
       


        protected override void OnModelCreating(ModelBuilder builder)
        {

            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(builder);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(GetConnectionString());
        }

        private string GetConnectionString()
        {
            IConfiguration config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true)
                .Build();
            return config["ConnectionStrings:DB"]!;
        }
    }
}

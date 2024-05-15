using Finbuckle.MultiTenant;
using Finbuckle.MultiTenant.Abstractions;
using Finbuckle.MultiTenant.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MultiTenantApplication.Entities;

namespace MultiTenantApplication
{
    public class AppDbContext : MultiTenantDbContext
    {
        public DbSet<Product> Products { get; set; }

        public AppDbContext(IMultiTenantContextAccessor multiTenantContextAccessor) : base(multiTenantContextAccessor)
        {
        }

        public AppDbContext(IMultiTenantContextAccessor multiTenantContextAccessor, DbContextOptions<AppDbContext> options) :
            base(multiTenantContextAccessor, options)
        {
        }
        public AppDbContext(ITenantInfo tenantInfo) : base(tenantInfo)
        {
        }

        public AppDbContext(ITenantInfo tenantInfo, DbContextOptions<AppDbContext> options) : base(tenantInfo, options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Product>().IsMultiTenant();
            modelBuilder.ConfigureMultiTenant();
            //modelBuilder.Entity<TenantInfo>().Property(ti => ti.Id).ValueGeneratedOnAdd(); ;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer("Server=.;Database=MultiTenantApplication;Trusted_Connection=True;TrustServerCertificate=True;Integrated Security=True;");
            base.OnConfiguring(optionsBuilder);
        }
    }
}

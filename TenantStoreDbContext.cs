using Finbuckle.MultiTenant.EntityFrameworkCore.Stores.EFCoreStore;
using Finbuckle.MultiTenant;
using Microsoft.EntityFrameworkCore;

namespace MultiTenantApplication
{
    public class TenantStoreDbContext : EFCoreStoreDbContext<TenantInfo>
    {
        public TenantStoreDbContext(DbContextOptions<TenantStoreDbContext> options) : base(options)
        {
        }
    }
}

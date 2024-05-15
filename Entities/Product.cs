using Finbuckle.MultiTenant;

namespace MultiTenantApplication.Entities
{
    [MultiTenant]
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}

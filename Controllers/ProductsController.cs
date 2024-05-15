using Finbuckle.MultiTenant;
using Microsoft.AspNetCore.Mvc;
using MultiTenantApplication.Entities;

namespace MultiTenantApplication.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class ProductsController : Controller
    {
        private AppDbContext _db;

        public ProductsController(AppDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_db.Products.ToList());
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok(_db.Products.FirstOrDefault(x => x.Id == id));
        }

        [HttpPost]
        public IActionResult Create(Product product)
        {
            var tenantInfo = HttpContext.GetMultiTenantContext<TenantInfo>().TenantInfo;
            _db.Products.Add(product);
            _db.SaveChanges();
            return Ok(product);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _db.Products.Remove(_db.Products.FirstOrDefault(x => x.Id == id));
            _db.SaveChanges();
            return NoContent();
        }
    }
}

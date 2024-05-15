using Microsoft.AspNetCore.Mvc;

namespace MultiTenantApplication.Controllers
{
    [ApiController]
    [Route("{__tenant__}/[controller]/[action]")]
    public class BaseTenantController : Controller
    {
    }
}

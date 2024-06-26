﻿using Finbuckle.MultiTenant;
using Finbuckle.MultiTenant.Abstractions;
using Microsoft.AspNetCore.Mvc;
using MultiTenantApplication.dtos;
using Newtonsoft.Json;

namespace MultiTenantApplication.Controllers
{
    [ApiController]
    public class TenantsController : Controller
    {
        TenantStoreDbContext _tenantContext;
        public TenantsController(TenantStoreDbContext tenantContext) { 
        _tenantContext = tenantContext;
        }
        [HttpGet]
        [Route("{__tenant__}/[action]")]
        public string Info()
        {
             var tenantInfo = HttpContext.GetMultiTenantContext<TenantInfo>().TenantInfo;

             if (tenantInfo != null)
             {
                 var tenantId = tenantInfo.Id;
                 var identifier = tenantInfo.Identifier;
                 var name = tenantInfo.Name;
             }

             return JsonConvert.SerializeObject(tenantInfo)!;
        }
        [HttpGet]
        [Route("[controller]/[action]")]
        public async Task<ITenantInfo> AddTenant([FromQuery] TenantDto tenant)
        {
            TenantInfo tenantInfo = new TenantInfo() { Id = Guid.NewGuid().ToString(), Identifier = tenant.Name };
            _tenantContext.Add(tenantInfo);
            await _tenantContext.SaveChangesAsync();
            return tenantInfo;
        }
    }
}

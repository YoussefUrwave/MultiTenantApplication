using Finbuckle.MultiTenant;
using Microsoft.EntityFrameworkCore;
using MultiTenantApplication;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var connection = builder.Configuration.GetConnectionString("sqlserver");
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("sqlserver"));
});
builder.Services.AddDbContext<TenantStoreDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("sqlserver"));
});
builder.Services.AddMultiTenant<TenantInfo>()
    .WithHeaderStrategy("tenant")
    .WithEFCoreStore<TenantStoreDbContext, TenantInfo>();

/*
.WithInMemoryStore(options =>
 {
     options.Tenants.Add(new TenantInfo { Id = "1", Identifier = "Apple"});
     options.Tenants.Add(new TenantInfo { Id = "2", Identifier = "Samsung" });
 });*/

var app = builder.Build();
app.UseMultiTenant();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

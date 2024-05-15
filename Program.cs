using Finbuckle.MultiTenant;
using Microsoft.EntityFrameworkCore;
using MultiTenantApplication;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

// Add services to the container.

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
    .WithRouteStrategy()
    .WithEFCoreStore<TenantStoreDbContext, TenantInfo>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();
app.UseMultiTenant();
app.MapControllers();

app.Run();

using IceCreamShopApi.Model;
using IceCreamShopApi.Utils;
using Microsoft.EntityFrameworkCore;

namespace IceCreamShopApi.Repository;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<MenuItem> TbMenu => Set<MenuItem>();
    public DbSet<OrderItem> TbOrders => Set<OrderItem>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<MenuItem>().HasKey(m => m.MenuId);
        modelBuilder.Entity<OrderItem>().HasKey(o => new { o.OrderId, o.MenuId });
        
        base.OnModelCreating(modelBuilder);

        foreach (var entity in modelBuilder.Model.GetEntityTypes())
        {
            var tableName = entity.GetTableName();
            if (!string.IsNullOrEmpty(tableName))
            {
                entity.SetTableName(tableName.ToSnakeCase());
            }
            
            foreach (var property in entity.GetProperties())
            {
                property.SetColumnName(property.GetColumnName().ToSnakeCase());
            }
        }
    }
}
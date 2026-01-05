using IceCreamShopApi;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? 
                       throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(connectionString)
    .LogTo(Console.WriteLine, LogLevel.Information));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapGet("/icecreamshop/menu", async (AppDbContext db) =>
    {
        var menuItemList = await db.TbMenu.ToListAsync();
        return new Menu(menuItemList);
    }).WithName("GetMenu");

app.MapPost("/icecreamshop/order", async (AppDbContext db, Order order) => 
    {
        await db.TbOrders.AddRangeAsync(order.OrderItemList);
        await db.SaveChangesAsync();
    }).WithName("PostOrder");

app.MapGet("/icecreamshop/orders", async (AppDbContext db) => 
    {
        var orderItemList = await db.TbOrders.ToListAsync();
        return new Order(orderItemList);
    }).WithName("GetOrders");

app.Run();

public record Menu(List<MenuItem> MenuItemList);

public record MenuItem(int MenuId, string Flavor, float Price);

public record Order(List<OrderItem> OrderItemList);

public record OrderItem(int OrderId, int MenuId, string ClientName, int Quantity, DateTime OrderDate);

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
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
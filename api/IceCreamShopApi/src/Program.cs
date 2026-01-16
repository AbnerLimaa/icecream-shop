using IceCreamShopApi.Repository;
using IceCreamShopApi.Model;
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
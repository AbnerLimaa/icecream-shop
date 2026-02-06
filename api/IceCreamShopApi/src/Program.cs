using IceCreamShopApi.Repository;
using IceCreamShopApi.Model.Logic.Query;
using IceCreamShopApi.Patterns;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddOpenApi();

builder.Services.AddScoped<MenuRepository>();
builder.Services.AddScoped<Mediator>();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}
app.UseHttpsRedirection();

app.MapGet("/icecreamshop/menu/{id:int}", async (int id, Mediator mediator) =>
{
    var request = new GetMenuQuery(id);
    var response = await mediator.MediateAsync(request);
    return response;
}).WithName("GetMenu");

//app.MapPost("/icecreamshop/order", async (Mediator mediator, Order order) => 
//    {
//        await db.TbOrders.AddRangeAsync(order.OrderItemList);
//        await db.SaveChangesAsync();
//    }).WithName("PostOrder");

//app.MapGet("/icecreamshop/orders", async (Mediator mediator) => 
//    {
//        var orderItemList = await db.TbOrders.ToListAsync();
//        return new Order(orderItemList);
//    }).WithName("GetOrders");

app.Run();
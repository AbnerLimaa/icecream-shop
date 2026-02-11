using IceCreamShopApi.Repository;
using IceCreamShopApi.Model.Query;
using IceCreamShopApi.Patterns;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddOpenApi();

builder.Services.AddScoped<MenuRepository>();
builder.Services.AddScoped<OrderRepository>();
builder.Services.AddScoped<Mediator>();

builder.Services.AddHealthChecks()
    .AddNpgSql(builder.Configuration.GetConnectionString("IceCreamShopDbConn")!);

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}
app.UseHttpsRedirection();

app.MapHealthChecks("/health");

app.MapGet("/icecreamshop/menu/{id:int}", async (int id, Mediator mediator) =>
{
    var request = new GetMenuWithItemsQuery(id);
    var response = await mediator.MediateAsync(request);
    return response;
}).WithName("GetMenu");

//app.MapPost("/icecreamshop/order", async (Mediator mediator, Order order) => 
//    {
//        await db.TbOrders.AddRangeAsync(order.OrderItemList);
//        await db.SaveChangesAsync();
//    }).WithName("PostOrder");

app.MapGet("/icecreamshop/orders", async (Mediator mediator) => 
    {
        var request = new GetAllOrdersByClientsQuery();
        var response = await mediator.MediateAsync(request);
        return response;
    }).WithName("GetAllOrdersByClients");

app.Run();
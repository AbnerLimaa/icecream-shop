using IceCreamShopApi.Model;
using IceCreamShopApi.Model.Query;
using IceCreamShopApi.Repository;

namespace IceCreamShopApi.Patterns;

public class Mediator(MenuRepository menuRepository, OrderRepository orderRepository)
{
    private readonly IReadOnlyDictionary<string, IHandler> _handlers = new Dictionary<string, IHandler>
    {
        { GetMenuWithItemsQuery.Id, new GetMenuWithItemsQuery.Handler(menuRepository) },
        { GetAllOrdersByClientsQuery.Id, new GetAllOrdersByClientsQuery.Handler(orderRepository) }
    };

    public async Task<IResponse> MediateAsync(IRequest request)
    {
        if (!_handlers.TryGetValue(request.RequestId, out var handler))
            throw new NotImplementedException($"Handler {request.RequestId} not implemented");
        
        return await handler.HandleAsync(request);
    }
}
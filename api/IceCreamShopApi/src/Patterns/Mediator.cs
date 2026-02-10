using IceCreamShopApi.Model.Logic;
using IceCreamShopApi.Model.Logic.Query;
using IceCreamShopApi.Repository;

namespace IceCreamShopApi.Patterns;

public class Mediator(MenuRepository menuRepository, OrderRepository orderRepository)
{
    private readonly IReadOnlyDictionary<string, IHandler> _handlers = new Dictionary<string, IHandler>
    {
        { GetMenuQuery.Id, new GetMenuQuery.Handler(menuRepository) },
        { GetAllOrdersByClientsQuery.Id, new GetAllOrdersByClientsQuery.Handler(orderRepository) }
    };

    public async Task<IResponse> MediateAsync(IRequest request)
    {
        if (!_handlers.TryGetValue(request.RequestId, out var handler))
            return new NoHandlerResponse();
        
        return await handler.HandleAsync(request);
    }
}
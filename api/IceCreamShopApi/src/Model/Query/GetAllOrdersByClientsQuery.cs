using IceCreamShopApi.Model.Result;
using IceCreamShopApi.Repository;

namespace IceCreamShopApi.Model.Query;

public class GetAllOrdersByClientsQuery : IRequest
{
    public const string Id = nameof(GetAllOrdersByClientsQuery);
    
    public string RequestId => Id;
    
    public class Handler(OrderRepository orderRepository) : IHandler
    {
        public string RequestId => Id;
        
        public async Task<IResponse> HandleAsync(IRequest request)
        {
            var ordersByClients = await orderRepository.GetAllOrdersByClientsAsync();
            var dictionary = new Dictionary<int, OrderByClientListResult>();
            
            foreach (var order in ordersByClients)
            {
                var orderByClient = new OrderByClientListResult.Adapter().Adapt(order);
                if (dictionary.TryGetValue(order.OrderId, out var viewModel))
                {
                    viewModel.MergeWith(orderByClient);
                    continue;
                }
                dictionary.Add(order.OrderId, orderByClient);
            }
            
            return new Response(new OrderByClientMultipleListResult(dictionary.Values));
        }
    }

    private record Response(OrderByClientMultipleListResult Result) : IResponse
    {
        public string Message => !Result.Any() ? "No order found" : $"Found {Result.Count()} orders";
    }
}
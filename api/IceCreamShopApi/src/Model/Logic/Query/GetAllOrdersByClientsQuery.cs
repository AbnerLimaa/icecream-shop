using IceCreamShopApi.Model.ViewModel;
using IceCreamShopApi.Repository;

namespace IceCreamShopApi.Model.Logic.Query;

public class GetAllOrdersByClientsQuery : IRequest
{
    public const string Id = "GetOrdersQuery";
    
    public string RequestId => Id;
    
    public class Handler(OrderRepository orderRepository) : IHandler
    {
        public string RequestId => Id;
        
        public async Task<IResponse> HandleAsync(IRequest request)
        {
            var ordersByClients = await orderRepository.GetAllOrdersByClientsAsync();
            var dictionary = new Dictionary<int, OrderByClientListViewModel>();
            
            foreach (var order in ordersByClients)
            {
                var orderByClient = new OrderByClientListViewModel.Adapter().Adapt(order);
                if (dictionary.TryGetValue(order.OrderId, out var viewModel))
                {
                    viewModel.MergeWith(orderByClient);
                    continue;
                }
                dictionary.Add(order.OrderId, orderByClient);
            }
            
            return new Response(dictionary.Values);
        }
    }

    private record Response(IEnumerable<OrderByClientListViewModel> OrderByClientList) : IResponse
    {
        public string Message => !OrderByClientList.Any() ? "No Order found" : "Found orders";
    }
}
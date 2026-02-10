using IceCreamShopApi.Model.Data;
using IceCreamShopApi.Patterns;

namespace IceCreamShopApi.Model.ViewModel;

public class OrderByClientListViewModel : IViewModel
{
    public int OrderId { get; }
    
    public string ClientName { get; }
    
    public string OrderDate { get; }

    private float _totalPrice;
    public string TotalPrice => $"{_totalPrice:C2}";

    public IReadOnlyList<OrderByClientViewModel> Items { get; private set; }

    private OrderByClientListViewModel(int orderId, string clientName, string orderDate, float totalPrice, IReadOnlyList<OrderByClientViewModel> items)
    {
        _totalPrice = totalPrice;
        
        OrderId = orderId;
        ClientName = clientName;
        OrderDate = orderDate;
        Items = items;
    }

    public void MergeWith(OrderByClientListViewModel merger)
    {
        if (merger.OrderId != OrderId) return;

        _totalPrice += merger._totalPrice;
        var newItems = new List<OrderByClientViewModel>();
        newItems.AddRange(Items);
        newItems.AddRange(merger.Items);
        Items = newItems;
    }

    public class Adapter : IAdapter<OrderByClient, OrderByClientListViewModel>
    {
        public OrderByClientListViewModel Adapt(OrderByClient source)
        {
            var orderDate = $"{source.OrderDate:dddd, MMMM dd, yyyy - hh:mm tt}";
            var item = new OrderByClientViewModel.Adapter().Adapt(source);

            return new OrderByClientListViewModel(
                orderId: source.OrderId, 
                clientName: source.ClientName, 
                orderDate: orderDate, 
                totalPrice: source.UnitPrice, 
                items: new List<OrderByClientViewModel> { item });
        }
    }
}
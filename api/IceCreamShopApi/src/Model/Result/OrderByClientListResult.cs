using IceCreamShopApi.Repository.Data;
using IceCreamShopApi.Patterns;

namespace IceCreamShopApi.Model.Result;

public class OrderByClientListResult : IResult
{
    public int OrderId { get; }
    
    public string ClientName { get; }
    
    public string OrderDate { get; }

    private float _totalPrice;
    public string TotalPrice => $"{_totalPrice:C2}";

    public IReadOnlyList<OrderByClientResult> Items { get; private set; }

    private OrderByClientListResult(int orderId, string clientName, string orderDate, float totalPrice, IReadOnlyList<OrderByClientResult> items)
    {
        _totalPrice = totalPrice;
        
        OrderId = orderId;
        ClientName = clientName;
        OrderDate = orderDate;
        Items = items;
    }

    public void MergeWith(OrderByClientListResult merger)
    {
        if (merger.OrderId != OrderId) return;

        _totalPrice += merger._totalPrice;
        var newItems = new List<OrderByClientResult>();
        newItems.AddRange(Items);
        newItems.AddRange(merger.Items);
        Items = newItems;
    }

    public class Adapter : IAdapter<OrderByClient, OrderByClientListResult>
    {
        public OrderByClientListResult Adapt(OrderByClient source)
        {
            var orderDate = $"{source.OrderDate:dddd, MMMM dd, yyyy - hh:mm tt}";
            var item = new OrderByClientResult.Adapter().Adapt(source);

            return new OrderByClientListResult(
                orderId: source.OrderId, 
                clientName: source.ClientName, 
                orderDate: orderDate, 
                totalPrice: source.UnitPrice, 
                items: new List<OrderByClientResult> { item });
        }
    }
}
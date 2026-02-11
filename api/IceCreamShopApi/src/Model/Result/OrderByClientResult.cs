using IceCreamShopApi.Repository.Data;
using IceCreamShopApi.Patterns;

namespace IceCreamShopApi.Model.Result;

public class OrderByClientResult : IResult
{
    public string Flavor { get; }

    private readonly float _unitPrice;
    public string UnitPrice => $"{_unitPrice:C2}";
    
    public int Quantity { get; }

    private OrderByClientResult(string  flavor, float unitPrice, int quantity)
    {
        _unitPrice = unitPrice;
        
        Flavor = flavor;
        Quantity = quantity;
    }

    public class Adapter : IAdapter<OrderByClient, OrderByClientResult>
    {
        public OrderByClientResult Adapt(OrderByClient source)
        {
            return new OrderByClientResult(source.Flavor, source.UnitPrice, source.Quantity);
        }
    }
}
using IceCreamShopApi.Repository.Data;
using IceCreamShopApi.Patterns;

namespace IceCreamShopApi.Model.Result;

public class OrderByClientResult : IResult
{
    public string Flavor { get; }
    
    public string UnitPrice { get; }
    
    public int Quantity { get; }

    private OrderByClientResult(string  flavor, string unitPrice, int quantity)
    {
        Flavor = flavor;
        UnitPrice = unitPrice;
        Quantity = quantity;
    }

    public class Adapter : IAdapter<OrderByClient, OrderByClientResult>
    {
        public OrderByClientResult Adapt(OrderByClient source)
        {
            var price = $"{source.UnitPrice:C2}";

            return new OrderByClientResult(source.Flavor, price, source.Quantity);
        }
    }
}
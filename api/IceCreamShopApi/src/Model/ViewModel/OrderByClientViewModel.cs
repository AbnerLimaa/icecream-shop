using IceCreamShopApi.Model.Data;
using IceCreamShopApi.Patterns;

namespace IceCreamShopApi.Model.ViewModel;

public class OrderByClientViewModel : IViewModel
{
    public string Flavor { get; }
    
    public string UnitPrice { get; }
    
    public int Quantity { get; }

    private OrderByClientViewModel(string  flavor, string unitPrice, int quantity)
    {
        Flavor = flavor;
        UnitPrice = unitPrice;
        Quantity = quantity;
    }

    public class Adapter : IAdapter<OrderByClient, OrderByClientViewModel>
    {
        public OrderByClientViewModel Adapt(OrderByClient source)
        {
            var price = $"{source.UnitPrice:C2}";

            return new OrderByClientViewModel(source.Flavor, price, source.Quantity);
        }
    }
}
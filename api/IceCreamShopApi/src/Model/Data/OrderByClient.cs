namespace IceCreamShopApi.Model.Data;

public record OrderByClient(int OrderId, string ClientName, string Flavor, float UnitPrice, int Quantity, DateTime OrderDate) : IData
{
    public OrderByClient() : this(0, "", "", 0.0f, 0, DateTime.MinValue) { }
}
namespace IceCreamShopApi.Model.Data;

public record ClientOrder(int OrderId, int ClientId, int MenuItemId, string Flavor, float Price, int Quantity, DateTime OrderDate) : IData;
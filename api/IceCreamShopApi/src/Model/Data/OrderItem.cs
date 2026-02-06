namespace IceCreamShopApi.Model.Data;

public record OrderItem(int OrderId, int MenuId, string ClientName, int Quantity, DateTime OrderDate) : IData;
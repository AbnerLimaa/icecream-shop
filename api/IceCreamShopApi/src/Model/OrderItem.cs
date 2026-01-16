namespace IceCreamShopApi.Model;

public record OrderItem(int OrderId, int MenuId, string ClientName, int Quantity, DateTime OrderDate);
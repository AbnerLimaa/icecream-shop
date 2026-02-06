namespace IceCreamShopApi.Model.Data;

public record MenuItem(int Id, string Flavor, float Price) : IData;
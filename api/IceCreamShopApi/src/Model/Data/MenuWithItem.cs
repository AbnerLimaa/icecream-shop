namespace IceCreamShopApi.Model.Data;

public record MenuWithItem(int MenuId, string MenuName, string Flavor, float Price) : IData
{
    public MenuWithItem() : this(0, "", "", 0.0f) { }
}
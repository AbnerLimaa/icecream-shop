namespace IceCreamShopApi.Repository.Data;

public class EmptyData : IData
{
    public static readonly  EmptyData Instance = new();
    
    private EmptyData() { }
}
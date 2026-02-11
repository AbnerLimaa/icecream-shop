namespace IceCreamShopApi.Model.Result;

public class EmptyResult : IResult
{
    public static readonly  EmptyResult Instance = new();
    
    private EmptyResult() { }
}
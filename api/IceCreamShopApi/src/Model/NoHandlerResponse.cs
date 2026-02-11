using IceCreamShopApi.Model.Result;
using IResult = IceCreamShopApi.Model.Result.IResult;

namespace IceCreamShopApi.Model;

public class NoHandlerResponse : IResponse
{
    public IResult ResponseData => EmptyResult.Instance;

    public string Message => "No Handler registered for this request";
}
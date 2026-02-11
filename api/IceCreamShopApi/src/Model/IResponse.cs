using IResult = IceCreamShopApi.Model.Result.IResult;

namespace IceCreamShopApi.Model;

public interface IResponse
{
    IResult ResponseData { get; }
    
    string Message { get; }
}
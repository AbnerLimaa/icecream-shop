namespace IceCreamShopApi.Model;

public interface IHandler
{
    string  RequestId { get; }
    
    Task<IResponse> HandleAsync(IRequest request);
}
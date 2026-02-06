using IceCreamShopApi.Model.ViewModel;

namespace IceCreamShopApi.Model.Logic;

public interface IHandler
{
    string  RequestId { get; }
    
    Task<IResponse> HandleAsync(IRequest request);
}
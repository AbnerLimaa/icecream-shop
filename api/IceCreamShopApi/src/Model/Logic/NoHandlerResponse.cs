using IceCreamShopApi.Model.Data;
using IceCreamShopApi.Model.ViewModel;

namespace IceCreamShopApi.Model.Logic;

public class NoHandlerResponse : IResponse
{
    public IViewModel ResponseData => EmptyViewModel.Instance;

    public string Message => "No Handler registered for this request";
}
using IceCreamShopApi.Model.Result;
using IceCreamShopApi.Repository;
using IResult = IceCreamShopApi.Model.Result.IResult;

namespace IceCreamShopApi.Model.Query;

public class GetMenuWithItemsQuery(int menuId) : IRequest
{
    private readonly int _menuId = menuId;
    public const string Id = nameof(GetMenuWithItemsQuery);
    
    public string RequestId => Id;

    public class Handler(MenuRepository menuRepository) : IHandler
    {
        public string RequestId => Id;
        
        public async Task<IResponse> HandleAsync(IRequest request)
        {
            var menuId = ((GetMenuWithItemsQuery)request)._menuId;
            var menuWithItems = await menuRepository.GetMenuWithItemsAsync(menuId);
            var result = new MenuWithItemListResult.Adapter().Adapt(menuWithItems);
            return new Response(result, menuId);
        }
    }

    private record Response(MenuWithItemListResult Result, int MenuId) : IResponse
    {
        public IResult ResponseData => Result;
        
        public string Message => Result.Items.Count == 0 ? $"No Menu found for menuId: {MenuId}" : $"Found menu with id: {MenuId}";
    }
}
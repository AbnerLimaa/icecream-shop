using IceCreamShopApi.Model.ViewModel;
using IceCreamShopApi.Repository;

namespace IceCreamShopApi.Model.Logic.Query;

public class GetMenuQuery(int menuId) : IRequest
{
    private readonly int _menuId = menuId;
    public const string Id = "GetMenuQuery";
    
    public string RequestId => Id;

    public class Handler(MenuRepository menuRepository) : IHandler
    {
        public string RequestId => Id;
        
        public async Task<IResponse> HandleAsync(IRequest request)
        {
            var menuId = ((GetMenuQuery)request)._menuId;
            var menuWithItems = await menuRepository.GetMenuWithItemsAsync(menuId);
            var menuWithItemsViewModel = new MenuWithItemListViewModel.Adapter().Adapt(menuWithItems);
            return new Response(menuWithItemsViewModel, menuId);
        }
    }

    private record Response(MenuWithItemListViewModel MenuWithItemList, int MenuId) : IResponse
    {
        public string Message => MenuWithItemList.Items.Count == 0 ? $"No Menu found for menuId: {MenuId}" : $"Found menu with id: {MenuId}";
    }
}
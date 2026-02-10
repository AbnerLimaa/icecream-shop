using IceCreamShopApi.Model.Data;
using IceCreamShopApi.Patterns;

namespace IceCreamShopApi.Model.ViewModel;

public class MenuWithItemListViewModel : IViewModel
{
    public static readonly MenuWithItemListViewModel Empty = new(string.Empty, new List<MenuWithItemViewModel>());
    
    public string Name { get; }
    
    public IReadOnlyCollection<MenuWithItemViewModel> Items { get; }
    
    private MenuWithItemListViewModel(string name, IReadOnlyCollection<MenuWithItemViewModel> items)
    {
        Name = name;
        Items = items;
    }
    
    public class Adapter : IAdapter<IEnumerable<MenuWithItem>, MenuWithItemListViewModel>
    {
        public MenuWithItemListViewModel Adapt(IEnumerable<MenuWithItem> source)
        {
            var menuWithItems = source.ToList();
            if (menuWithItems.Count == 0)
                return Empty;
            
            var items = menuWithItems.Select(item => new MenuWithItemViewModel.Adapter().Adapt(item)).ToList();
            
            return new MenuWithItemListViewModel(menuWithItems.First().MenuName, items);
        }
    }
}
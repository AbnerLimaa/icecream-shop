using IceCreamShopApi.Repository.Data;
using IceCreamShopApi.Patterns;

namespace IceCreamShopApi.Model.Result;

public class MenuWithItemListResult : IResult
{
    public static readonly MenuWithItemListResult Empty = new(string.Empty, new List<MenuWithItemResult>());
    
    public string Name { get; }
    
    public IReadOnlyCollection<MenuWithItemResult> Items { get; }
    
    private MenuWithItemListResult(string name, IReadOnlyCollection<MenuWithItemResult> items)
    {
        Name = name;
        Items = items;
    }
    
    public class Adapter : IAdapter<IEnumerable<MenuWithItem>, MenuWithItemListResult>
    {
        public MenuWithItemListResult Adapt(IEnumerable<MenuWithItem> source)
        {
            var menuWithItems = source.ToList();
            if (menuWithItems.Count == 0)
                return Empty;
            
            var items = menuWithItems.Select(item => new MenuWithItemResult.Adapter().Adapt(item)).ToList();
            
            return new MenuWithItemListResult(menuWithItems.First().MenuName, items);
        }
    }
}
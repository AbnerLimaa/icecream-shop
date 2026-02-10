using IceCreamShopApi.Model.Data;
using IceCreamShopApi.Patterns;

namespace IceCreamShopApi.Model.ViewModel;

public class MenuWithItemViewModel : IViewModel
{
    public string Flavor { get; }

    public string Price { get; }
    
    private MenuWithItemViewModel(string flavor, string price)
    {
        Flavor = flavor;
        Price = price;
    }

    public class Adapter : IAdapter<MenuWithItem, MenuWithItemViewModel>
    {
        public MenuWithItemViewModel Adapt(MenuWithItem source)
        {
            var price = $"{source.Price:C2}";

            return new MenuWithItemViewModel(source.Flavor, price);
        }
    }
}